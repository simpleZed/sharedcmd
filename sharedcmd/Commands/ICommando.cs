using System.Collections.Generic;

namespace sharedcmd.Commands
{
    public interface ICommando
    {
        string? Command { get; }

        string? Arguments { get; }

        void AddCommand(string command);

        void AddCommands(IEnumerable<string> sequence);
    }
}