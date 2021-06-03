﻿using System.Collections.Generic;
using System.Diagnostics;

using sharedcmd.Commands;
using sharedcmd.Extensions;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners.Shells
{
    public abstract class ShellBase<T> : IRunner, ICommander<T> where T : ArgumentBase, new()
    {
        public IDictionary<string, string>? EnvironmentVariables { get; set; }

        protected ShellBase()
        {
            EnvironmentVariables = new Dictionary<string, string>();
        }

        public virtual string BuildArgument(T argument)
        {
            return argument.ToString();
        }

        public abstract ICommando GiveOrder();

        public virtual string Run(IRunOptions options)
        {
            var process = CreateProcess(options);
            process.PopulateEnvironment(EnvironmentVariables!);
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }

        private static Process CreateProcess(IRunOptions options)
        {
            return new()
            {
                StartInfo = new()
                {
                    FileName = options.Command,
                    Arguments = options.Arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
        }
    }
}