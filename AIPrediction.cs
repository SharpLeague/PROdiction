using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using LeagueSharp;
using LeagueSharp.Common;
using PROdiction;
using SharpDX;
using Collision = LeagueSharp.Common.Collision;

namespace PROdiction
{
    public class AIPredictionInput
    {
        private static readonly List<string> LaneMinions = new List<string>
        {
            "SRU_ChaosMinionRanged",
            "SRU_ChaosMinionMelee",
            "SRU_ChaosMinionSiege",
            "SRU_OrderMinionRanged",
            "SRU_OrderMinionMelee",
            "SRU_OrderMinionSiege",
        };

        public PredictionInput input;
        public Vector3 CastPosition;

        public static float SpeedFromVelocity(Vector3 velocity)
        {
            var realVelocity = new Vector3
            {
                X = velocity.X * 20,
                Y = velocity.Y * 20,
                Z = velocity.Z * 20
            };

            return new Vector3
            {
                X = 0,
                Y = 0,
                Z = 0
            }.Distance(realVelocity);
        }

        private void PutHistoryPath(ICollection<float> target, OnNewPathEvent @event, Obj_AI_Hero targetHero)
        {
            var lastElement = @event.Path.LastOrDefault();
            var firstElement = @event.Path.FirstOrDefault();

            target.Add(firstElement.Distance(lastElement));
            target.Add(@event.Path.Length);
            target.Add((float) (Game.Time - @event.GameTime)); // time ago
            target.Add(targetHero.ServerPosition.X - lastElement.X);
            target.Add(targetHero.ServerPosition.Y - lastElement.Y);
        }
        
        public float GetDelay()
        {
            return input.Delay + input.From.Distance(CastPosition) / input.Speed + Game.Ping / 1000.0f;
        }
        
        public float[] GetValues()
        {
            var delay = GetDelay();
            
            var values = new List<float>();
            values.Add(ObjectManager.Player.Direction.X);
            values.Add(ObjectManager.Player.Direction.Y);
            values.Add(input.Unit.Direction.X);
            values.Add(input.Unit.Direction.X);
            values.Add(delay); // delay
            values.Add(input.Unit.MoveSpeed);
            values.Add(SpeedFromVelocity(input.Unit.Velocity)); // speed
            values.Add(input.Unit.IsWindingUp ? 1.0f : 0.0f);
            values.Add(input.Unit.IsMelee ? 1.0f : 0.0f);
            values.Add(input.Unit.IsDashing() ? 1.0f : 0.0f); // is dash
            values.Add(input.Unit.Health / input.Unit.MaxHealth); // health
            values.Add(input.Unit.MaxHealth);
            
            var amountOfPaths = 0;
            
            try
            {
                var lastPathes = Pathes.LastNewPathes[input.Unit.NetworkId];

                amountOfPaths = lastPathes.Count;

                IEnumerable<OnNewPathEvent> lastNewPathesEnumerable = lastPathes;
                
                foreach (var path in lastNewPathesEnumerable.Reverse().Take(10))
                {
                    PutHistoryPath(values, path, (Obj_AI_Hero) input.Unit);
                }
            }
            catch (KeyNotFoundException)
            {
            }

            for (var i = 0; i < 10 - amountOfPaths; i++)
            {
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
            }

            // values.Add(input.Range);
            values.Add(input.Radius);
            values.Add(input.Type == SkillshotType.SkillshotLine ? 1.0f : 0.0f);

            var minions = ObjectManager.Get<Obj_AI_Minion>().ToList();
            values.Add(minions.Count(minion =>
                input.Unit.ServerPosition.Distance(minion.ServerPosition) < 600.0 &&
                minion.Team != input.Unit.Team &&
                minion.Health > 0.0 && minion.MaxHealth > 0.0 && LaneMinions.Contains(minion.Name)));

            values.Add(NavMesh.IsWallOfGrass(HeroManager.Player.ServerPosition, 10) &&
                       !NavMesh.IsWallOfGrass(input.Unit.ServerPosition, 10)
                ? 1.0f
                : 0.0f);

            var minimumHealth = minions.Where(minion =>
                    minion.IsValid &&
                    input.Unit.ServerPosition.Distance(minion.ServerPosition) < 1000.0 &&
                    minion.Team != input.Unit.Team &&
                    minion.Health > 0.0 && minion.MaxHealth > 0.0 && LaneMinions.Contains(minion.Name))
                .MinOrDefault(minion => minion.Health);
            values.Add(minimumHealth?.Health ?? 2000.0f);

            values.Add(DashList.IsDashAvailable((Obj_AI_Hero) input.Unit, delay) ? 1.0f : 0.0f);
            
            PutEnemyStatistics(values);
            
            values.Add(input.Unit.BoundingRadius);

            values.Add((float) ChampionSuccessRatio.GetChampionSuccessRatio(((Obj_AI_Hero) input.Unit).ChampionName));
            
            values.Add(SpellList.GetDangerLevel(((Obj_AI_Hero) input.Unit).ChampionName, input.Slot)); // danger level

            values.Add(((Obj_AI_Hero) input.Unit).GetSpellSlot("summonerflash") != SpellSlot.Unknown
                ? 1.0f
                : 0.0f); // has_flash

            return values.ToArray();
        }

        private void PutEnemyStatistics(ICollection<float> values)
        {
            var champions = ObjectManager.Get<Obj_AI_Hero>()
                .Where(champion => champion.ServerPosition.Distance(input.Unit.ServerPosition) < 1000.0f).ToList();

            var targetAllies = champions.Where(champion => champion.Team == input.Unit.Team
                                                           && input.Unit.NetworkId != champion.NetworkId).ToList();
            values.Add(targetAllies.Count);

            var targetEnemies = champions.Where(champion =>
                champion.Team != input.Unit.Team && input.Unit.NetworkId != champion.NetworkId).ToList();
            values.Add(targetEnemies.Count);

            var allyHpRatio = targetAllies.Count > 0
                ? (targetAllies.Select(ally => ally.Health / ally.MaxHealth).Sum() / targetAllies.Count)
                : 0.0f;
            var enemyHpRatio = targetEnemies.Count > 0
                ? (targetEnemies.Select(enemy => enemy.Health / enemy.MaxHealth).Sum() / targetEnemies.Count)
                : 0.0f;

            values.Add(allyHpRatio);
            values.Add(enemyHpRatio);
        }

        public static float[] GetClosestWall(Vector3 position, Obj_AI_Base source, Obj_AI_Hero target)
        {
            var direction = (target.ServerPosition - source.ServerPosition).Normalized();

            for (var radius = 50; radius < 1000; radius += 50)
            {
                foreach (var currentPosition in Program.CirclePoints(20, radius, position)
                    .Where(currentPosition => currentPosition.IsWall()))
                {
                    return new[]
                    {
                        target.ServerPosition.To2D().AngleBetween(direction.To2D()),
                        currentPosition.Distance(position)
                    };
                }
            }

            return new[] {0.0f, 2000.0f};
        }
    }

    public class AIPrediction
    {
        private static readonly Model CirclePathModel = Model.Load("keras2cpp_0_success_path");
        private static readonly Model CirclePositionModel = Model.Load("keras2cpp_0_success_position");

        private static readonly Model LinePathModel = Model.Load("keras2cpp_1_success_path");
        private static readonly Model LinePositionModel = Model.Load("keras2cpp_1_success_position");
        
        static Vector3 GetPositionOnPath(PredictionInput input, List<Vector2> path, float speed = -1)
        {
            speed = (Math.Abs(speed - (-1)) < float.Epsilon) ? Prediction.SpeedFromVelocity(input.Unit) : speed;

            var pLength = path.PathLength();

            if (pLength < 5)
            {
                return input.Unit.ServerPosition;
            }

            var realRadius = input.Radius + input.Unit.BoundingRadius;

            //Skillshots with only a delay
            var tDistance = input.Delay * speed - (input.Radius * 0.75f);
            if (pLength >= tDistance && Math.Abs(input.Speed - float.MaxValue) < float.Epsilon)
            {
                for (var i = 0; i < path.Count - 1; i++)
                {
                    var a = path[i];
                    var b = path[i + 1];
                    var d = a.Distance(b);

                    if (d >= tDistance)
                    {
                        var direction = (b - a).Normalized();

                        var cp = a + direction * tDistance;

                        return cp.To3D();
                    }

                    tDistance -= d;
                }
            }

            //Skillshot with a delay and speed.
            if (pLength >= tDistance && Math.Abs(input.Speed - float.MaxValue) > float.Epsilon)
            {
                var d = tDistance;
                if (input.Type == SkillshotType.SkillshotLine || input.Type == SkillshotType.SkillshotCone)
                {
                    if (input.From.Distance(input.Unit.ServerPosition, true) < 200 * 200)
                    {
                        d = input.Delay * speed;
                    }
                }

                path = path.CutPath(d);
                var tT = 0f;
                for (var i = 0; i < path.Count - 1; i++)
                {
                    var a = path[i];
                    var b = path[i + 1];
                    var tB = a.Distance(b) / speed;
                    var direction = (b - a).Normalized();
                    a = a - speed * tT * direction;
                    var sol = Geometry.VectorMovementCollision(a, b, speed, input.From.To2D(), input.Speed, tT);
                    var t = (float) sol[0];
                    var pos = (Vector2) sol[1];

                    if (pos.IsValid() && t >= tT && t <= tT + tB)
                    {
                        if (pos.Distance(b, true) < 20)
                            break;

                        return pos.To3D();
                    }

                    tT += tB;
                }
            }

            var position = path.Last();
            return position.To3D();
        }

        private static Dictionary<SpellSlot, MinMaxValues> MIN_MAX = new Dictionary<SpellSlot, MinMaxValues>();
        
        public static PredictionOutput GetPrediction(PredictionInput input)
        {
            MinMaxValues values;
            if (!MIN_MAX.TryGetValue(input.Slot, out values))
            {
                values = new MinMaxValues();
                MIN_MAX[input.Slot] = values;
            }

            var positionOnPath =
                GetPositionOnPath(input, input.Unit.GetWaypoints(), Prediction.SpeedFromVelocity(input.Unit));

            var pathInput = new AIPredictionInput
            {
                input = input,
                CastPosition = positionOnPath
            };

            // var pathValues = pathInput.GetValues();
            // Console.WriteLine("Inputs: " + pathValues.Length);
            //
            // for (var i = 0; i < pathValues.Length; i++)
            // {
            //     Console.Write(pathValues[i] + ", ");
            // }
            //
            // Console.WriteLine();


            var positionInput = new AIPredictionInput
            {
                input = input,
                CastPosition = input.Unit.ServerPosition
            };

            var outputPath =
                (input.Type == SkillshotType.SkillshotLine ? LinePathModel : CirclePathModel).Predict(
                    pathInput.GetValues());
            // var outputPosition =
            //     (input.Type == SkillshotType.SkillshotLine ? LinePositionModel : CirclePositionModel).Predict(
            //         positionInput.GetValues());
            
            float hitchance;
            Vector3 castPosition;
            // if (outputPosition[0] > outputPath[0])
            // {
            //     castPosition = input.Unit.ServerPosition;
            //     hitchance = outputPosition[0];
            // }
            // else
            // {
                castPosition = positionOnPath;
                hitchance = outputPath[0];
            // }

            var normalized = values.Normalized(hitchance);
            
            var output = new PredictionOutput
            {
                HitchanceFloat = normalized,
                CastPosition = castPosition
            };

            Console.WriteLine(input.Slot + " == " + normalized + " ! " + outputPath[0] + " || " + pathInput.GetDelay());

            if (input.Collision)
            {
                var maxCollisions = 0;
                if (HeroManager.Player.ChampionName == "Lux")
                {
                    maxCollisions = 1;
                }

                var positions = new List<Vector3> {output.CastPosition};
                output.CollisionObjects = Collision.GetCollision(positions, input);
                output.CollisionObjects.RemoveAll(x => x.NetworkId == input.Unit.NetworkId);
                if (output.CollisionObjects.Count > maxCollisions)
                {
                    output.HitchanceFloat = 0.0f;
                    output.Hitchance = HitChance.Collision;
                }
            }

            return output;
        }
    }
}