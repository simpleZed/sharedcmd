using System;

using sharedcmd;

dynamic cmd = new Cmd();
var env = new (string, string)[]
{
  ("jow", @"C:\"),
  ("Hello", "B")
};
cmd._Env(env);
var setA = cmd["SET jow"]();
var setPath = cmd["SET Hello"]();
Console.WriteLine(setA);
Console.WriteLine(setPath);