namespace sharedcmd.Runners.Arguments
{
    public class CmdArgument : ArgumentBase
    {
        protected override string BuildFlag() => Flag is null ? null! : $"/{Flag}";
    }
}