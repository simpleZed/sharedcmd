using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace sharedcmd.Runners.Arguments
{
    public abstract class ArgumentBase : IArgument, IEquatable<ArgumentBase?>
    {
        public string? Flag { get; set; }

        public object? ValueObject { get; set; }

        public string? Value => ValueObject is string s ? Environment.ExpandEnvironmentVariables(s) : null!;

        protected virtual string BuildFlag()
        {
            if (Flag is null)
            {
                return null!;
            }
            var prefix = Flag.Length is 1 ? "-" : "--";
            return prefix + Flag;
        }

        public override string ToString()
        {
            var flag = BuildFlag();
            if (string.IsNullOrWhiteSpace(flag) && string.IsNullOrWhiteSpace(Value))
            {
                return null!;
            }
            if (string.IsNullOrWhiteSpace(flag))
            {
                return Value!;
            }
            return string.IsNullOrWhiteSpace(Value) ? flag : $"{flag} {Value}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return Equals(obj as ArgumentBase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(ArgumentBase? other)
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
        public static bool operator ==(ArgumentBase? left, ArgumentBase? right)
        {
            return EqualityComparer<ArgumentBase>.Default.Equals(left!, right!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(ArgumentBase? left, ArgumentBase? right)
        {
            return !(left == right);
        }
    }
}