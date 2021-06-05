using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    /// <summary>
    /// A smart class that can execute commands on powershell on windows (powershell.exe)
    /// </summary>
    public class Powershell : CliBase<CommandOption>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Powershell"/> class.
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute the powershell commands.
        /// </param>
        public Powershell(ShellBase<CommandOption> shell = null!) : base(shell ?? new PsShell())
        {
        }
    }
}