using sharedcmd.Commands;
using sharedcmd.Runners.Options;

namespace sharedcmd.Runners
{
    /// <summary>
    /// Abstracts a smart contract that whoever implement it can generate command objects,
    /// from string arguments.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the argument parser to use.
    /// </typeparam>
    public interface ICommander<in T> where T : CommandOption
    {
        /// <summary>
        /// Given an argument returns it string representation.
        /// </summary>
        /// <param name="argument">
        /// Argument to stringify.
        /// </param>
        /// <returns>
        /// The string version of an argument.
        /// </returns>
        string BuildOption(T argument);

        /// <summary>
        /// Gives an order to the cli.
        /// </summary>
        /// <returns>
        /// The command to be stored and executed.
        /// </returns>
        ICommando GenerateCommand();
    }
}