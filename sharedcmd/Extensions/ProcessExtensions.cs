using System.Collections.Generic;
using System.Diagnostics;

namespace sharedcmd.Extensions
{
    internal static class ProcessExtensions
    {
        internal static void PopulateEnvironment(this Process process, IDictionary<string, string> environmentVariables)
        {
            foreach (var variable in environmentVariables)
            {
                process.StartInfo.EnvironmentVariables[variable.Key] = variable.Value;
            }
        }
    }
}
