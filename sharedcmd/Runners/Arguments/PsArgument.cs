namespace sharedcmd.Runners.Arguments
{
    public class PsArgument : ArgumentBase
    {
        protected override string BuildFlag()
        {
            return Flag is null ? null! : $"-{Flag}";
        }
    }
}