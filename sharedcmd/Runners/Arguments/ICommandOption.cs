namespace sharedcmd.Runners.Arguments
{
    /// <summary>
    /// Abstracts a smart contract that whoever
    /// implements it must know how to parse options.
    /// </summary>
    public interface ICommandOption
    {
        /// <summary>
        /// The prefix of a given flag or a value in case the flag isn't provided.
        /// </summary>
        string? Prefix { get; }

        /// <summary>
        /// The command option.
        /// </summary>
        string? Flag { get; }
    }
}