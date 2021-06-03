namespace sharedcmd.Runners.Arguments
{
    public class BashArgument : ArgumentBase
    {
        protected override string BuildFlag()
        {
            return Flag is null ? null! : $"-{Flag}";
        }
    }
}