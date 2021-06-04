namespace sharedcmd.Runners.Arguments
{
    public interface IArgument
    {
        string? Prefix { get; }

        string? Flag { get; }
    }
}