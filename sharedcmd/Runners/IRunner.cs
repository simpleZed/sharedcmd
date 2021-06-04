using System.Collections.Generic;

namespace sharedcmd.Runners
{
    /// <summary>
    /// Abstracts a smart contract that whoever implement
    /// it will be able to run processes given an option.
    /// </summary>
    public interface IRunner
    {
        /// <summary>
        /// The process environment variables.
        /// </summary>
        IDictionary<string, string>? EnvironmentVariables { get; set; }

        /// <summary>
        /// Runs the process.
        /// </summary>
        /// <param name="options">
        /// Options of the process.
        /// </param>
        /// <returns>
        /// The text result that was redirected from the process.
        /// </returns>
        string Run(IRunOptions options);
    }
}