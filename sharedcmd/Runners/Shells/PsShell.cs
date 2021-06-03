using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    public class PsShell : ShellBase<PsArgument>
    {
        public override ICommando GiveOrder()
        {
            return new PsCommando(this);
        }
    }
}