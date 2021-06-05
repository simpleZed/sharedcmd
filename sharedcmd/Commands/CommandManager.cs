using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Manages commands on the CLI.
    /// </summary>
    public sealed class CommandManager : IManageCommand
    {
        private readonly List<string> commands = new();

        public IReadOnlyCollection<string> Commands => commands;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCommand(string command)
        {
            commands.Add(command);
        }

        public void AddCommands(IEnumerable<string> sequence)
        {
            if (sequence.Any())
            {
                commands.AddRange(sequence);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCommands(params string[] sequence)
        {
            AddCommands(sequence.AsEnumerable());
        }
    }
}