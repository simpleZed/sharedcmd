namespace sharedcmd.Runners
{
    /// <summary>
    /// Abstracts a smart contract that is responsible to handle the process options.
    /// </summary>
    public interface IRunOptions
    {
        /// <summary>
        /// The name of cli command (cmd, bash, powershell).
        /// </summary>
        string? Command { get; set; }

        /// <summary>
        /// Arguments that will be used to enrich the command.
        /// </summary>
        string? Arguments { get; set; }
    }
}