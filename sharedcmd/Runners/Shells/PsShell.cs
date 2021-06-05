using sharedcmd.Commands;
using sharedcmd.Runners.Options;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Represents a class that can execute commands on a powershell.
    /// </summary>
    public class PsShell : ShellBase<CommandOption>
    {
        public override ICommando GenerateCommand()
        {
            return new PsCommando(this);
        }
    }
}