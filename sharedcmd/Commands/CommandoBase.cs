using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Extensions;
using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public abstract class CommandoBase<T> : DynamicObject, ICommando where T : Argument, new()
    {
        protected readonly List<string> commands = new();

        protected readonly List<T> arguments = new();

        private readonly ShellBase<T> shell;

        public string? Command => commands.FirstOrDefault();

        public string? Arguments => commands.Skip(1)
                                            .Concat(arguments.Select(a => shell.BuildArgument(a))
                                                             .Where(s => !string.IsNullOrWhiteSpace(s)))
                                            .Aggregate((x, y) => $"{x} {y}");

        protected CommandoBase(ShellBase<T> shell)
        {
            this.shell = shell;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCommand(string command)
        {
            commands.Add(command);
        }

        public void AddCommands(IEnumerable<string> sequence)
        {
            if (sequence.Any())
            {
                commands.AddRange(sequence);
            }
        }

        public void AddCommands(params string[] sequence)
        {
            if (sequence is string[] { Length: > 0})
            {
                commands.AddRange(sequence);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            AddCommand(binder.Name);
            result = this;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            AddCommands(indexes.OfType<string>());
            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var parsedArguments = binder.ParseArguments<T>(args);
            arguments.AddRange(parsedArguments);
            AddCommands(parsedArguments.Select(a => a.ToString()));
            result = shell.Run(new RunOptions(this));
            return true;
        }
    }
}