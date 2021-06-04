using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public class CmdCommando : CommandoBase<Argument>
    {
        public CmdCommando(ShellBase<Argument> runner) : base(runner)
        {
            AddCommands("cmd", "/c");
        }
    }
}