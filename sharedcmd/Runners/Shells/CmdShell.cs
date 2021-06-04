﻿using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Represents a class that can execute commands on a cmd shell.
    /// </summary>
    public class CmdShell : ShellBase<CommandOption>
    {
        public override ICommando GiveOrder()
        {
            return new CmdCommando(this);
        }
    }
}