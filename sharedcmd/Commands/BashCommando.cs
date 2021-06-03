using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public class BashCommando : CommandoBase<BashArgument>
    {
        public BashCommando(ShellBase<BashArgument> shell) : base(shell)
        {
            AddCommands("bash", "-c");
        }
    }
}