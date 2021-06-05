using sharedcmd.Commands;
using sharedcmd.Runners.Options;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Represents a class that can execute commands on a bash shell.
    /// </summary>
    public class BashShell : ShellBase<CommandOption>
    {
        public override ICommando GenerateCommand()
        {
            return new BashCommando(this);
        }
    }
}