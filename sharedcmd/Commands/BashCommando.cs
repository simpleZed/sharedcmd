using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Represents a class that represents a bash command.
    /// </summary>
    public class BashCommando : CommandoBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="BashCommando"/>.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute this command.
        /// </param>
        public BashCommando(ShellBase<CommandOption> shell) : base(shell)
        {
            Manager.AddCommands("bash", "-c");
        }
    }
}