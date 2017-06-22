using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLibrary4Net
{
    public class RandomGenerator
    {
        private static readonly Random Random = new Random(Environment.TickCount);

        public static string RandomString(int length, string characters = "abcdefghijklimnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            Throws.IfNullOrEmpty(characters);
            return new String(Enumerable.Repeat(characters, length).Select(_ => _[Random.Next(characters.Length)]).ToArray());
        }

        public static T RandomElement<T>(IEnumerable<T> elements)
        {
            Throws.IfNullOrEmpty(elements);
            return elements.ElementAt(Random.Next(0, elements.Count()));
        }

        public static int RandomInt(int from, int to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            return Random.Next(from, to);
        }

        public static long RandomLong(long from, long to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            return long.MaxValue;
        }

        public static float RandomFloat(float from, float to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            var range = to - from;
            var sample = Random.NextDouble();
            return (float)((sample * range) + from);
        }

        public static double RandomDouble(double from, double to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            var range = to - from;
            var sample = Random.NextDouble();
            return (sample * range) + from;
        }

        public static DateTime RandomTime(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            if(from.Kind != to.Kind)
            {
                throw new ArgumentException($"from.Kind({from.Kind}) should be same with to.Kind({to.Kind})");
            }

            return new DateTime(RandomLong(from.Ticks, to.Ticks), from.Kind);
        }

        public static DateTimeOffset RandomTime(DateTimeOffset from, DateTimeOffset to)
        {
            if (from >= to)
            {
                throw new ArgumentException($"from({from}) should be less then to({to})");
            }

            if(from.Offset != to.Offset)
            {
                throw new ArgumentException($"from.Offset({from.Offset}) should be same with to.Offset({to.Offset})");
            }

            return new DateTimeOffset(RandomLong(from.Ticks, to.Ticks), from.Offset);
        }
    }
}
