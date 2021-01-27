using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LeagueSharp;
using LeagueSharp.Common;
using PROdiction;
using SharpDX;

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

        private static int[] BuffTypes =
        {
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            17,
            18,
            19,
            20,
            21,
            22,
            23,
            24,
            25,
            27,
            28,
            29,
            30,
            31,
            32,
            33,
            34
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

        private void PutHistoryPath(ICollection<float> target, OnNewPathEvent @event)
        {
            var lastElement = @event.Path.LastOrDefault();
            var firstElement = @event.Path.FirstOrDefault();
            var secondElement = @event.Path.Skip(1).FirstOrDefault();

            target.Add(firstElement.Distance(lastElement));
            target.Add(@event.Path.Length);

            var pathLength = @event.Path.Select(vector => vector.To2D()).ToList().PathLength();
            target.Add(pathLength); // length
            target.Add(firstElement.IsZero
                ? 0.0f
                : HeroManager.Player.Direction.To2D().AngleBetween((secondElement - firstElement).To2D()));
            target.Add((float) (Game.Time - @event.GameTime)); // time ago
            target.Add(@event.Path.Length > 0 ? input.Unit.Distance(@event.Path[0]) : 0.0f);
            if (lastElement.IsZero)
            {
                target.Add(input.Unit.ServerPosition.X - lastElement.X);
                target.Add(input.Unit.ServerPosition.Y - lastElement.Y);
            }
            else
            {
                target.Add(0.0f);
                target.Add(0.0f);
            }

            target.Add(input.Unit.ServerPosition.Distance(HeroManager.Player.ServerPosition));
        }

        private void PutBuffData(ICollection<float> target, BuffInstance[] buffs)
        {
            var gameTime = Game.Time;
            foreach (var buffType in BuffTypes)
            {
                var maxBuff = buffs.Where(buff => (BuffType) buffType == buff.Type)
                    .Select(buff => buff.EndTime - gameTime)
                    .Select(buffTime => buffTime < 0 ? 0 : buffTime)
                    .OrderByDescending(buffTime => buffTime)
                    .FirstOrDefault();

                target.Add(maxBuff);
            }
        }

        public float[] GetValues()
        {
            var delay = input.Delay + input.From.Distance(CastPosition) / input.Speed + Game.Ping / 1000.0f;

            var speed = SpeedFromVelocity(input.Unit.Velocity);
            if (speed == 0.0)
            {
                speed = input.Unit.MoveSpeed;
            }

            var values = new List<float>();
            // values.Add(ChampionToId[Source.ChampionName]); // source champ
            // values.Add(ChampionToId[Target.ChampionName]); // target champ
            // values.Add(SourceSpellSlot); // source spell slot
            values.Add(input.Unit.Direction.X); // target direction x
            values.Add(input.Unit.Direction.Y); // target direction y
            values.Add(HeroManager.Player.Direction.X); // source direction x
            values.Add(HeroManager.Player.Direction.Y); // source direction y
            values.Add(delay); // delay
            values.Add(speed); // speed
            values.Add(delay * speed); // move area
            values.Add(input.Unit.IsWindingUp ? 1.0f : 0.0f);
            values.Add(input.Unit.IsMelee ? 1.0f : 0.0f);
            values.Add(input.Unit.IsDashing() ? 1.0f : 0.0f); // is dash
            values.Add(input.Unit.AttackRange);
            values.Add(input.Unit.Health / input.Unit.MaxHealth); // health
            values.Add(input.Unit.MaxHealth);
            values.Add(input.Unit.Direction.To2D()
                .AngleBetween(HeroManager.Player.Direction.To2D())); // angle_between_last_path_and_position
            values.Add(HeroManager.Player.ServerPosition.Distance(input.Unit.ServerPosition));

            // PutItemData(values, Target.InventoryItems);

            // PutSpellData(values, Target.GetSpell(SpellSlot.Q));
            // PutSpellData(values, Target.GetSpell(SpellSlot.W));
            // PutSpellData(values, Target.GetSpell(SpellSlot.E));
            // PutSpellData(values, Target.GetSpell(SpellSlot.R));
            // PutSpellData(values, Target.GetSpell(SpellSlot.Summoner1));
            // PutSpellData(values, Target.GetSpell(SpellSlot.Summoner2));

            PutBuffData(values, input.Unit.Buffs);

            var amountOfPaths = 0;

            var clicksPerSecond = 0;
            var clicksLen = 0.0f;

            try
            {
                var lastPathes = Pathes.LastNewPathes[input.Unit.NetworkId];

                amountOfPaths = lastPathes.Count;

                IEnumerable<OnNewPathEvent> lastNewPathesEnumerable = lastPathes;

                foreach (var path in lastNewPathesEnumerable.Reverse())
                {
                    if (Game.Time - path.GameTime < 1.0)
                    {
                        clicksPerSecond += 1;
                        clicksLen += Vector2.Zero.Distance(path.Path.LastOrDefault().To2D());
                    }
                }

                foreach (var path in lastNewPathesEnumerable.Reverse().Take(20))
                {
                    PutHistoryPath(values, path);
                }

                if (clicksPerSecond > 0)
                {
                    clicksLen /= clicksPerSecond;
                }
                else
                {
                    clicksLen = 0.0f;
                }
            }
            catch (KeyNotFoundException)
            {
            }

            for (var i = 0; i < 20 - amountOfPaths; i++)
            {
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
                values.Add(0.0f);
            }

            values.Add(input.Range);
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

            // var rangeToAllyTurret = ObjectManager.Get<Obj_AI_Turret>()
            //     .Where(turret => turret.Team == input.Unit.Team && turret.Health > 0 &&
            //                      input.Unit.ServerPosition.Distance(turret.Position) < 1000)
            //     .Select(turret => input.Unit.ServerPosition.Distance(turret.Position))
            //     .MinOrDefault(turretDistance => turretDistance);
            // // values.Add(rangeToAllyTurret == 0 ? 2000.0f : rangeToAllyTurret);
            // values.Add(2000.0f);
            //
            // var rangeToEnemyTurret = ObjectManager.Get<Obj_AI_Turret>()
            //     .Where(turret => turret.Team != input.Unit.Team && turret.Health > 0 &&
            //                      input.Unit.ServerPosition.Distance(turret.Position) < 1000.0)
            //     .Select(turret => input.Unit.ServerPosition.Distance(turret.Position))
            //     .MinOrDefault(turretDistance => turretDistance);
            // // values.Add(rangeToEnemyTurret == 0 ? 2000.0f : rangeToEnemyTurret);
            // values.Add(2000.0f);

            PutEnemyStatistics(values);

            values.Add(clicksPerSecond);

            values.Add(input.Unit.BoundingRadius);

            values.Add(clicksLen);

            values.Add(SpellList.GetDangerLevel(((Obj_AI_Hero) input.Unit).ChampionName, input.Slot)); // danger level
            values.Add(0.0f); // min max speed diff
            // values.AddRange(GetClosestWall(input.Unit.ServerPosition, HeroManager.Player, (Obj_AI_Hero) input.Unit));

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

    public class AIPredictionOutput
    {
        public float HitchancePath;
        public float HitchancePosition;
    }

    public class AIPrediction
    {
        private static readonly Model CirclePathModel = Model.Load("C://test/keras2cpp.0.success_path.model");
        private static readonly Model CirclePositionModel = Model.Load("C://test/keras2cpp.0.success_position.model");

        private static readonly Model LinePathModel = Model.Load("C://test/keras2cpp.1.success_path.model");
        private static readonly Model LinePositionModel = Model.Load("C://test/keras2cpp.1.success_position.model");

        [DllImport("libkeras2cpp.dll", SetLastError = true)]
        private static extern uint ReadMXCSR();

        [DllImport("libkeras2cpp.dll", SetLastError = true)]
        private static extern void SetMXCSR(uint value);

        public static AIPredictionOutput GetPrediction(PredictionInput input, Vector3 castPosition)
        {
            var delay = input.Delay + input.From.Distance(castPosition) / input.Speed;

            var aiInput = new AIPredictionInput
            {
                input = input,
                CastPosition = castPosition
            };

            var values = aiInput.GetValues();

            Console.WriteLine("Inputs: " + values.Length);
            //
            // for (var i = 0; i < values.Length; i++)
            // {
            //     Console.Write(values[i] + ", ");
            // }

            // Console.WriteLine();

            var outputPath =
                (input.Type == SkillshotType.SkillshotLine ? LinePathModel : CirclePathModel).Predict(values);
            var outputPosition =
                (input.Type == SkillshotType.SkillshotLine ? LinePositionModel : CirclePositionModel).Predict(values);

            Console.WriteLine(outputPath[0] + " -- " + outputPosition[0] + " || " + delay);

            SetMXCSR(0x1f80);

            return new AIPredictionOutput
            {
                HitchancePath = outputPath[0],
                HitchancePosition = outputPosition[0]
            };
        }
    }
}