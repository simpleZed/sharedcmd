using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Powershell : Cli<Argument>
    {
        public Powershell(ShellBase<Argument> shell = null!) : base(shell ?? new PsShell())
        {
        }
    }
}