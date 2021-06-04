using System;

namespace sharedcmd.Playground
{
    internal static class Program
    {
        private static void Main()
        {
            dynamic cmd = new Cmd();
            cmd._Env(("VARIABLE", "Hello\n"));
            var result = cmd["SET VARIABLE"]();
            Console.WriteLine(result);
        }
    }
}