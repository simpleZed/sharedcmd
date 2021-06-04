﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace sharedcmd.Runners.Arguments
{
    public class Argument : IArgument, IEquatable<Argument?>
    {
        public string? Flag { get; set; }

        public object? ValueObject { get; set; }

        public string? Value => ValueObject is string s ? Environment.ExpandEnvironmentVariables(s) : null!;

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Flag) && string.IsNullOrWhiteSpace(Value))
            {
                return null!;
            }
            if (string.IsNullOrWhiteSpace(Flag))
            {
                return Value!;
            }
            return string.IsNullOrWhiteSpace(Value) ? Flag! : Flag + Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return Equals(obj as Argument);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Argument? other)
        {
            return other != null &&
                   Flag == other.Flag &&
                   Value == other.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Flag, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Argument? left, Argument? right)
        {
            return EqualityComparer<Argument>.Default.Equals(left!, right!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Argument? left, Argument? right)
        {
            return !(left == right);
        }
    }
}