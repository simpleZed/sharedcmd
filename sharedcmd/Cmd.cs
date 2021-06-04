using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    /// <summary>
    /// A smart class that can execute commands on command prompt on windows (cmd.exe)
    /// </summary>
    public class Cmd : CliBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Cmd"/> class.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute the cmd commands.
        /// </param>
        public Cmd(ShellBase<CommandOption> shell = null!) : base(shell ?? new CmdShell())
        {
        }
    }
}