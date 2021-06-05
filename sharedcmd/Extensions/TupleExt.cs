using System;
using System.Collections.Generic;

namespace sharedcmd.Extensions
{
    public static class TupleExt
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey key, TValue value) tuple)
        {
            return new()
            {
                [tuple.key] = tuple.value
            };
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this Tuple<TKey, TValue> tuple)
        {
            return new()
            {
                [tuple.Item1] = tuple.Item2
            };
        }
    }
}