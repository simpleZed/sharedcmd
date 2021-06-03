namespace sharedcmd.Runners
{
    public interface IRunOptions
    {
        string? Command { get; set; }

        string? Arguments { get; set; }
    }
}