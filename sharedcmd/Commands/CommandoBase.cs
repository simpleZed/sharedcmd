﻿using System.Collections.Generic;
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
        protected readonly List<string> commands = new();

        private readonly ShellBase<T> shell;

        public string? Command => commands.FirstOrDefault();

        public string? Arguments => commands.Skip(1)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddCommands(params string[] sequence)
        {
            AddCommands(sequence.AsEnumerable());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            AddCommand(binder.Name);
            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            AddCommands(indexes.OfType<string>());
            result = this;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            AddUserCommands(binder, args);
            result = shell.Run(new RunOptions(this));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddUserCommands(InvokeBinder binder, object[] args)
        {
            var parsedArguments = binder.ParseOptions<T>(args);
            AddCommands(parsedArguments.Select(a => a.ToString()));
        }
    }
}