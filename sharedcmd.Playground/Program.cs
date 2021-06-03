using System;

namespace sharedcmd.Playground
{
    internal static class Program
    {
        private static void Main()
        {
            dynamic ps = new Powershell();
            var ls = ps.ls;
            Console.WriteLine(ls());
        }
    }
}
