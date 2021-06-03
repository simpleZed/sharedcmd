namespace sharedcmd.Runners.Arguments
{
    public class PsArgument : ArgumentBase
    {
        protected override string BuildFlag() => Flag is null ? null! : $"-{Flag}";
    }
}