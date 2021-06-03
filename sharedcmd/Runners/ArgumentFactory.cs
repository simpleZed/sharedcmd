﻿using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners
{
    internal static class ArgumentFactory
    {
        internal static T Of<T>(string flag, object value) where T : ArgumentBase, new()
        {
            return new()
            {
                Flag = flag,
                ValueObject = value
            };
        }
    }
}