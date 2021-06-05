using System.Collections.Generic;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Abstracts a smart class that can manage commands.
    /// </summary>
    public interface IManageCommand
    {
        /// <summary>
        /// The commands that can be executed by the CLI.
        /// </summary>
        IReadOnlyCollection<string> Commands { get; }

        /// <summary>
        /// Adds a new command to the cli.
        /// </summary>
        /// <param name="command">
        /// Command to be added on cli.
        /// </param>
        void AddCommand(string command);

        /// <summary>
        /// Adds a sequence of command to the cli.
        /// </summary>
        /// <param name="commands">
        /// Sequence of command to be added on cli.
        /// </param>
        void AddCommands(IEnumerable<string> commands);

        /// <summary>
        /// Adds a sequence of command to the cli.
        /// </summary>
        /// <param name="commands">
        /// Sequence of command to be added on cli.
        /// </param>
        void AddCommands(params string[] commands);
    }
}