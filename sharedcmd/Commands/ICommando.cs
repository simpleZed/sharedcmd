using System.Collections.Generic;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Abstracts a smart contract that whoever implement
    /// must be a valid command.
    /// </summary>
    public interface ICommando
    {
        /// <summary>
        /// Identifies which CLI the client is using.
        /// </summary>
        string? Command { get; }

        /// <summary>
        /// The command arguments.
        /// </summary>
        string? Arguments { get; }

        /// <summary>
        /// Adds a new command
        /// </summary>
        /// <param name="command"></param>
        void AddCommand(string command);

        /// <summary>
        /// Adds a new sequence of command.
        /// </summary>
        /// <param name="sequence">
        /// The sequence to add.
        /// </param>
        void AddCommands(IEnumerable<string> sequence);
    }
}