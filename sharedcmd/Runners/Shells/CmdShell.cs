using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    public class CmdShell : ShellBase<Argument>
    {
        public override ICommando GiveOrder()
        {
            return new CmdCommando(this);
        }
    }
}