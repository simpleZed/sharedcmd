using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Represents a class that represents a powershell command.
    /// </summary>
    public class PsCommando : CommandoBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="PsCommando"/>.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute this command.
        /// </param>
        public PsCommando(ShellBase<CommandOption> shell) : base(shell)
        {
            Manager.AddCommand("powershell");
        }
    }
}