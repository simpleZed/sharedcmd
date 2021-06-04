using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Cmd : Cli<Argument>
    {
        public Cmd(ShellBase<Argument> shell = null!) : base(shell ?? new CmdShell())
        {
        }
    }
}