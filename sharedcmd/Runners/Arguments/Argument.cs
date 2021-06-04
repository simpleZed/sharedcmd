using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace sharedcmd.Runners.Arguments
{
    public class Argument : IArgument, IEquatable<Argument?>
    {
        public string? Prefix { get; set; }

        public string? Flag { get; set; }

        public virtual string BuildPrefix()
        {
            return Prefix!;
        }

        public override string ToString()
        {
            var prefix = BuildPrefix();
            if (string.IsNullOrWhiteSpace(prefix) && string.IsNullOrWhiteSpace(Flag))
            {
                return null!;
            }
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return Flag!;
            }
            return string.IsNullOrWhiteSpace(Prefix) ? prefix! : prefix + Flag;
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
                   Prefix == other.Prefix &&
                   Flag == other.Flag;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Prefix, Flag);
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