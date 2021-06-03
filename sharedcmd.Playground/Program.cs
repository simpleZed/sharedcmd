using System;

namespace sharedcmd.Playground
{
    internal static class Program
    {
        private static void Main()
        {
            dynamic cmd = new Cmd();
            var result = cmd["git"].init();
            Console.WriteLine(result);
        }
    }
}
