namespace sharedcmd.Runners.Arguments
{
    public class CmdArgument : ArgumentBase
    {
        protected override string BuildFlag()
        {
            return Flag is null ? null! : $"/{Flag}";
        }
    }
}