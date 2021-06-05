using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Represents a class that represents a cmd command.
    /// </summary>
    public class CmdCommando : CommandoBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="CmdCommando"/>.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute this command.
        /// </param>
        public CmdCommando(ShellBase<CommandOption> shell) : base(shell)
        {
            AddCommands("cmd", "/c");
        }
    }
}