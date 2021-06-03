using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public class Cmd : Cli<CmdArgument>
    {
        public Cmd(ShellBase<CmdArgument> shell = null!) : base(shell ?? new CmdShell())
        {
        }
    }
}