using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Extensions;
using sharedcmd.Runners;
using sharedcmd.Runners.Options;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Commands
{
    /// <summary>
    /// Base class of all commands.
    /// </summary>
    /// <typeparam name="T">
    /// Type of argument parser to use.
    /// </typeparam>
    public abstract class CommandoBase<T> : DynamicObject, ICommando where T : CommandOption, new()
    {
        private readonly ShellBase<T> shell;

        public IManageCommand Manager { get; }

        public string? Command => Manager.Commands.FirstOrDefault();

        public string? Arguments => Manager.Commands.Skip(1)
                                                    .Aggregate((x, y) => $"{x} {y}");

        protected CommandoBase(ShellBase<T> shell, IManageCommand manager = null!)
        {
            this.shell = shell;
            Manager = manager ?? new CommandManager();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Manager.AddCommand(binder.Name);
            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            Manager.AddCommands(indexes.OfType<string>());
            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            AddBinding(binder, args);
            result = shell.Run(new RunOptions(this));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddBinding(InvokeBinder binder, object[] args)
        {
            var parsedArguments = binder.ParseOptions<T>(args);
            Manager.AddCommands(parsedArguments.Select(a => a.ToString()));
        }
    }
}