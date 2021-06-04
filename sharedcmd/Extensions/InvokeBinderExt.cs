using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Extensions
{
    public static class InvokeBinderExt
    {
        public static IEnumerable<T> ParseArguments<T>(this InvokeBinder binder, object[] args)
            where T : Argument, new()
        {
            var (names, argsCount, namesCount) = binder.FetchInfo();
            var allNames = Enumerable.Repeat<string>(null!, argsCount - namesCount).Concat(names);
            return allNames.Zip(args, (f, v) => ArgumentFactory.Of<T>(v.ToString(), f))
                           .OrderBy(a => a.Flag);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static (IReadOnlyCollection<string> names, int argsCount, int namesCount)
            FetchInfo(this InvokeBinder binder)
        {
            var (names, count) = (binder.CallInfo.ArgumentNames, binder.CallInfo.ArgumentCount);
            return (names, count, names.Count);
        }
    }
}