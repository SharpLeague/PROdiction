﻿using System;
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
        private readonly float TIME_DIFFRENCE = 30.0f;

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
                    .Where(value => gameTime - value.Time < TIME_DIFFRENCE)
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
                    .Where(value => gameTime - value.Time < TIME_DIFFRENCE)
                    .Select(value => value.Value)
                    .Max();
            }
        }

        private void RemoveOld()
        {
            var gameTime = Game.Time;
            Values
                .RemoveAll(value => gameTime - value.Time > TIME_DIFFRENCE);
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
            
            if (Values.Count < 8)
            {
                return (float) Math.Min(0.25, normalized);
            }

            return normalized;
        }
    }
}