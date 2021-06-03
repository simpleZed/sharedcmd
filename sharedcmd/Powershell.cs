using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Powershell : Cli<PsArgument>
    {
        public Powershell(ShellBase<PsArgument> shell = null!) : base(shell ?? new PsShell())
        {
        }
    }
}