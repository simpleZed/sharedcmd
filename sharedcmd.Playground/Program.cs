using System;

namespace sharedcmd.Playground
{
    internal static class Program
    {
        private static void Main()
        {
            dynamic ps = new Bash();
            var ls = ps["ls"];
            Console.WriteLine(ls());
        }
    }
}