using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace PROdiction
{
    public class ValueTime
    {
        public float Value;
        public float Time;
    }

    public class MinMaxValues
    {
        private List<ValueTime> Values { get; set; } = new List<ValueTime>();
        private readonly float COUNT_TO_KEEP = 100;

        private float Minimum
        {
            get
            {
                if (Values.Count == 0)
                {
                    return 0.0f;
                }
                
                var gameTime = Game.Time;
                return Values
                    .Select(value => value.Value)
                    .Min();
            }
        }

        private float Maximum
        {
            get
            {
                if (Values.Count == 0)
                {
                    return 0.13f;
                }
                
                var gameTime = Game.Time;
                return Values
                    .Select(value => value.Value)
                    .Max();
            }
        }

        private void RemoveOld()
        {
            Values = Values.OrderByDescending(valueTime => valueTime.Time).Take(100).ToList();
        }

        public void Record(float value)
        {
            Values
                .Add(new ValueTime
                {
                    Value = value,
                    Time = Game.Time
                });
        }

        public float Normalized(float value)
        {
            Record(value);
            RemoveOld();
            
            var min = Minimum;
            var max = Maximum;

            var normalized = (value - min) / (max - min);
            
            if (Values.Count < 8 && value <= 0.75)
            {
                return (float) Math.Min(0.25, normalized);
            }

            return normalized;
        }
    }
}