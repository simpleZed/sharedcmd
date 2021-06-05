using sharedcmd.Commands;
using sharedcmd.Runners.Options;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Represents a class that can execute commands on a cmd shell.
    /// </summary>
    public class CmdShell : ShellBase<CommandOption>
    {
        public override ICommando GenerateCommand()
        {
            return new CmdCommando(this);
        }
    }
}