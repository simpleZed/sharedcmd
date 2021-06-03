using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    public class BashShell : ShellBase<BashArgument>
    {
        public override ICommando GiveOrder()
        {
            return new BashCommando(this);
        }
    }
}