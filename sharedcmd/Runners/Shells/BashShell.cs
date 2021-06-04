using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Represents a class that can execute commands on a bash shell.
    /// </summary>
    public class BashShell : ShellBase<CommandOption>
    {
        public override ICommando GiveOrder()
        {
            return new BashCommando(this);
        }
    }
}