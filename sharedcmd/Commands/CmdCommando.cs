using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public sealed class CmdCommando : CommandoBase<CmdArgument>
    {
        public CmdCommando(ShellBase<CmdArgument> runner) : base(runner)
        {
            AddCommand("cmd");
            AddCommand("/c");
        }
    }
}
