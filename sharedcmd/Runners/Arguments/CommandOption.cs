using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace sharedcmd.Runners.Arguments
{
    /// <summary>
    /// Represents a class that knows how to parse command options.
    /// </summary>
    public class CommandOption : ICommandOption, IEquatable<CommandOption?>
    {
        public string? Prefix { get; set; }

        public string? Flag { get; set; }

        /// <summary>
        /// Applies some necessary transformation to the command prefix or command value.
        /// </summary>
        /// <returns>
        /// The command prefix/value.
        /// </returns>
        public virtual string BuildPrefix()
        {
            return Prefix is string s ? Environment.ExpandEnvironmentVariables(s) : Prefix!;
        }

        public override string ToString()
        {
            var prefix = BuildPrefix();
            return (prefix, Flag) switch 
            {
                (null, null) => null!,
                (var x, var y) => x + y,
                (var x, null) => x,
                (null, var y) => y
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return Equals(obj as CommandOption);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(CommandOption? other)
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
        public static bool operator ==(CommandOption? left, CommandOption? right)
        {
            return EqualityComparer<CommandOption>.Default.Equals(left!, right!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(CommandOption? left, CommandOption? right)
        {
            return !(left == right);
        }
    }
}
