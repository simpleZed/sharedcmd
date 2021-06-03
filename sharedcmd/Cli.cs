﻿using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    public abstract class Cli<T> : DynamicObject where T : ArgumentBase, new()
    {
        private readonly ShellBase<T> shell;

        protected Cli(ShellBase<T> shell)
        {
            this.shell = shell;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var commando = shell.GiveOrder();
            commando.AddCommand(binder.Name);
            result = commando;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var commando = shell.GiveOrder();
            var commands = indexes.OfType<string>()
                                  .Where(s => !string.IsNullOrWhiteSpace(s));
            commando.AddCommands(commands);
            result = commando;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null!;
            if (binder.Name is "_Env")
            {
                if (args.Length is not 1)
                {
                    return false;
                }
                shell.EnvironmentVariables = args[0] as IDictionary<string, string>;
                return true;
            }
            return false;
        }
    }
}