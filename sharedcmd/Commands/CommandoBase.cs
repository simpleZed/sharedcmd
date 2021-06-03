using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    public abstract class CommandoBase<T> : DynamicObject, ICommando where T : ArgumentBase, new()
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCommands(IEnumerable<string> sequence)
        {
            if (sequence.Any())
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var (names, argsCount, namesCount) = FetchInfo(binder);
            var allNames = Enumerable.Repeat<string>(null!, argsCount - namesCount).Concat(names);
            arguments.AddRange(allNames.Zip(args, (s, o) => ArgumentFactory.Of<T>(s, o)));
            result = shell.Run(new RunOptions(this));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private (IReadOnlyCollection<string> names, int argsCount, int namesCount) FetchInfo(InvokeBinder binder)
        {
            var (names, count) = (binder.CallInfo.ArgumentNames, binder.CallInfo.ArgumentCount);
            return (names, count, names.Count);
        }
    }
}