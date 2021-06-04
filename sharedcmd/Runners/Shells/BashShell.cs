using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    public class BashShell : ShellBase<Argument>
    {
        public override ICommando GiveOrder()
        {
            return new BashCommando(this);
        }
    }
}