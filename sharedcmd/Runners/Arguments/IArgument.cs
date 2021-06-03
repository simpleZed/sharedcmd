namespace sharedcmd.Runners.Arguments
{
    public interface IArgument
    {
        string? Flag { get; }

        string? Value { get; }
    }
}