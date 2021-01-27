using System.Collections.Generic;
using System.IO;
using SharpDX;

namespace PROdiction
{
    using System;
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;

    static class Program
    {
        private static Menu _mainMenu;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        public static List<Vector3> CirclePoints(float CircleLineSegmentN, float radius, Vector3 position)
        {
            List<Vector3> points = new List<Vector3>();
            for (var i = 1; i <= CircleLineSegmentN; i++)
            {
                var angle = i * 2 * Math.PI / CircleLineSegmentN;
                var point = new Vector3(position.X + radius * (float) Math.Cos(angle),
                    position.Y + radius * (float) Math.Sin(angle), position.Z);
                points.Add(point);
            }

            return points;
        }

        public static SpellSlot GetSpellSlot(this Obj_AI_Hero unit, string name)
        {
            using (var enumerator = unit.Spellbook.Spells
                .Where(spell => string.Equals(spell.Name, name, StringComparison.CurrentCultureIgnoreCase))
                .GetEnumerator())
            {
                if (enumerator.MoveNext())
                    return enumerator.Current.Slot;
            }

            return SpellSlot.Unknown;
        }

        private static void OnLoad(EventArgs args)
        {
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnUpdate += GameOnOnUpdate;
        }

        private static void GameOnOnUpdate(EventArgs args)
        {
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
        }
    }
}