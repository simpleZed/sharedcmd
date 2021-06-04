using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public class BashCommando : CommandoBase<Argument>
    {
        public BashCommando(ShellBase<Argument> shell) : base(shell)
        {
            AddCommands("bash", "-c");
        }
    }
}