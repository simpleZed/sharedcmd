using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using sharedcmd.Commands;

namespace sharedcmd.Runners
{
    public class RunOptions : IRunOptions, IEquatable<RunOptions?>
    {
        public string? Command { get; set; }

        public string? Arguments { get; set; }

        public RunOptions(ICommando commando)
        {
            Command = commando.Command;
            Arguments = commando.Arguments;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"{Command} {Arguments}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object? obj)
        {
            return Equals(obj as RunOptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(RunOptions? other)
        {
            return other != null &&
                   Command == other.Command &&
                   Arguments == other.Arguments;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return HashCode.Combine(Command, Arguments);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RunOptions? left, RunOptions? right)
        {
            return EqualityComparer<RunOptions>.Default.Equals(left!, right!);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RunOptions? left, RunOptions? right)
        {
            return !(left == right);
        }
    }
}