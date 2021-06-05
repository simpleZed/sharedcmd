using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using sharedcmd.Commands;
using sharedcmd.Extensions;
using sharedcmd.Runners.Options;

namespace sharedcmd.Runners.Shells
{
    /// <summary>
    /// Base class of all Shell classes.
    /// </summary>
    /// <typeparam name="T">
    /// Type of argument parser to use when using a given shell.
    /// </typeparam>
    public abstract class ShellBase<T> : IRunner, ICommander<T> where T : CommandOption, new()
    {
        public virtual IDictionary<string, string>? EnvironmentVariables { get; set; }

        protected ShellBase()
        {
            EnvironmentVariables = new Dictionary<string, string>();
        }

        public virtual string BuildOption(T option)
        {
            return option.ToString();
        }

        public abstract ICommando FindCommand();

        public virtual string Run(IRunOptions options)
        {
            var process = StartProcess(options);
            return ReadFromProcess(process);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Process StartProcess(IRunOptions options)
        {
            var process = CreateProcess(options);
            process.PopulateEnvironment(EnvironmentVariables!);
            process.Start();
            return process;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string ReadFromProcess(Process process)
        {
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}