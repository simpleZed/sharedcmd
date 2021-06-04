using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public class PsCommando : CommandoBase<Argument>
    {
        public PsCommando(ShellBase<Argument> shell) : base(shell)
        {
            AddCommand("powershell");
        }
    }
}