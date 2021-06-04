using System;

namespace sharedcmd.Playground
{
    internal static class Program
    {
        private static void Main()
        {
            dynamic cmd = new Cmd();
            var clone = cmd.git["log"]();
            Console.WriteLine(clone);
        }
    }
}