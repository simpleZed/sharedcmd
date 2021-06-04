using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    /// <summary>
    /// A smart class that can execute commands on bash on WSL, macOS, Linux.
    /// </summary>
    public class Bash : CliBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Bash"/> class.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute the bash commands.
        /// </param>
        public Bash(ShellBase<CommandOption> shell = null!) : base(shell ?? new BashShell())
        {
        }
    }
}