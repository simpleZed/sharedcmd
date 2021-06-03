using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public sealed class PsCommando : CommandoBase<PsArgument>
    {
        public PsCommando(ShellBase<PsArgument> shell) : base(shell)
        {
        }
    }
}