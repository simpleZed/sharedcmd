using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners
{
    public static class ArgumentFactory
    {
        public static T Of<T>(string prefix, object flag) where T : Argument, new()
        {
            return new()
            {
                Prefix = prefix,
                Flag = flag is string f ? f : null!
            };
        }
    }
}