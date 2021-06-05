using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd
{
    /// <summary>
    /// Base class of all cli classes.
    /// </summary>
    /// <typeparam name="T">
    /// Type of argument that the cli understand.
    /// </typeparam>
    public abstract class CliBase<T> : DynamicObject where T : CommandOption, new()
    {
        private readonly ShellBase<T> shell;

        /// <summary>
        /// Creates a new instance of <see cref="CliBase{T}"/>
        /// </summary>
        /// <param name="shell">
        /// The shell that will execute the commands.
        /// </param>
        protected CliBase(ShellBase<T> shell)
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
                shell.EnvironmentVariables = args[0] switch
                {
                    IEnumerable<(string, string)> sequence => sequence.Select(a => a)
                                                                      .ToDictionary(t => t.Item1, t => t.Item2),
                    ValueTuple<string, string> tuple => args.OfType<(string, string)>()
                                                            .ToDictionary(t => t.Item1, t => t.Item2),
                    Tuple<string, string> tuple => args.OfType<(string, string)>()
                                                       .ToDictionary(t => t.Item1, t => t.Item2),
                    Dictionary<string, string> dictionary => dictionary,
                    _ => new()
                };
                return true;
            }
            return false;
        }
    }
}