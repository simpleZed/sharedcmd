using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Bash : Cli<BashArgument>
    {
        public Bash(ShellBase<BashArgument> shell = null!) : base(shell ?? new BashShell())
        {
        }
    }
}