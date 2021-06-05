using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Commands;
using sharedcmd.Extensions;
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
            result = BindTo(binder.Name);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            var commands = indexes.OfType<string>()
                                  .Where(s => !string.IsNullOrWhiteSpace(s));
            result = BindTo(commands);
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null!;
            if (binder.Name is "_Env" && args.Length is 1)
            {
                shell.EnvironmentVariables = ToEnvVariable(args);
                return true;
            }
            return false;
        }

        private Dictionary<string, string> ToEnvVariable(object[] args)
        {
            return args[0] switch
            {
                IEnumerable<(string, string)> sequence => sequence.ToDictionary(t => t.Item1, t => t.Item2),
                ValueTuple<string, string> tuple => tuple.ToDictionary(),
                Tuple<string, string> tuple => tuple.ToDictionary(),
                Dictionary<string, string> dictionary => dictionary,
                _ => new()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICommando BindTo(string command)
        {
            var commando = shell.GenerateCommand();
            commando.Manager.AddCommand(command);
            return commando;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICommando BindTo(IEnumerable<string> commands)
        {
            var commando = shell.GenerateCommand();
            commando.Manager.AddCommands(commands);
            return commando;
        }
    }
}