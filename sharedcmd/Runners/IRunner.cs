using System.Collections.Generic;

namespace sharedcmd.Runners
{
    public interface IRunner
    {
        IDictionary<string, string>? EnvironmentVariables { get; set; }

        string Run(IRunOptions options);
    }
}