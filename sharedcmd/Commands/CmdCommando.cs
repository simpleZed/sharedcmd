using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public class CmdCommando : CommandoBase<CmdArgument>
    {
        public CmdCommando(ShellBase<CmdArgument> runner) : base(runner)
        {
            AddCommands(new[] { "cmd", "/c" });
        }
    }
}