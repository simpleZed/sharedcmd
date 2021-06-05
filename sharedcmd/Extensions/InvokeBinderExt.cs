using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

using sharedcmd.Runners;
using sharedcmd.Runners.Options;

namespace sharedcmd.Extensions
{
    public static class InvokeBinderExt
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ParseArguments<T>(this InvokeBinder binder, object[] args)
            where T : CommandOption, new()
        {
            var (names, argsCount, namesCount) = binder.FetchInfo();
            var allNames = Enumerable.Repeat<string>(null!, argsCount - namesCount).Concat(names);
            return allNames.Zip(args, (f, p) => CommandOptionFactory.Of<T>(p.ToString(), f))
                           .OrderBy(a => a.Prefix);
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