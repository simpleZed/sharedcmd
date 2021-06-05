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
        /// Manages the commands on CLI.
        /// </summary>
        IManageCommand Manager { get; }
    }
}