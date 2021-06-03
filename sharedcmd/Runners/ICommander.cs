using sharedcmd.Commands;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners
{
    public interface ICommander<in T> where T : ArgumentBase
    {
        string BuildArgument(T argument);

        ICommando GiveOrder();
    }
}