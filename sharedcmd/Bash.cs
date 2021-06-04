using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Bash : Cli<Argument>
    {
        public Bash(ShellBase<Argument> shell = null!) : base(shell ?? new BashShell())
        {
        }
    }
}