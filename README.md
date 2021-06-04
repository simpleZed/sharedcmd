# shared-cmd #

A rewritting of cmd, but for .NET Standard 2.0 only

A C# Library to run external programs / commands in a simpler way. It is inspired from the [cmd](https://github.com/manojlds/cmd) library
that is used to showcase the features of C# DLR (Dynamic Language Runtime) and it's heavily inspired on [sh](https://github.com/amoffat/sh) Python library.

**How to get it?**

Shared Cmd is available through the Nuget Package Manager.

Or, you can build it from source.

**How to use it?**

Create a dynamic instance of Cmd/Powershell/Bash or create your own:

```csharp
dynamic cmd = new Cmd();
dynamic powershell = new Powershell();
dynamic bash = new Bash();
```

Now, you can call commands off cmd:

```csharp
cmd.git.clone("http://github.com/manojlds/cmd");
cmd["git clone"]("http://github.com/manojlds/cmd");
```

The above would be equivalent to `git clone http://github.com/manojlds/cmd`.

You can pass flags by naming the arguments:

```csharp
/*
Not possible, due to some command prompts do not work only with - and --, 
for e.g on windows command prompt you may prefix a command with '/'.
So the client decides how the flag prefix will be made.
*/
cmd.git.log(grep: "test"); //This line don't work, it will be translated to cmd /c git log testgrep.
cmd.git.log["--grep"]("test"); //Use this instead.
cmd.git.log("test", grep: "--"); //or this.
cmd.git.log(grep: "--", test: true); //you can trick the compiler.
cmd.git.log["--grep test"](); //it simply works.
cmd["git log --grep test"](); //this is particularly useful at building your own cmd. No support to redirecting input though.
```

The above would be equivalent to `git log --grep test`

Also, non-string values are ignored and if there is no flag, the argument is not considered.

You can call multiple commands off the same instance of cmd:

```csharp
var gitOutput = cmd.git();
var svnOutput = cmd.svn();
```

Note that the commands can be case sensitive, and as such `cmd.git` is not same as, say, `cmd.Git`.

**How to set environment variables?**

Environment variables can be set for the process by calling `._Env` method on an instance of Cli and pass the set of environment variables with their values as a 
`ValueTuple<string, string>, Tuple<string, string>, Dictionary<string, string>, IEnumerable<(string, string)>`:

```csharp
cmd._Env(("PATH", @"C:\"));
```
# Note that this replaces existing variables with the new values.

```csharp
var env = new (string, string)[]
{
  ("PATH", @"C:\"),
  ("A", "B")
};
cmd._Env(env);
```

**Shells**

You can use cmd to run command on, well, cmd, Powershell, Bash. Choose the shell you want to use while creating cmd:

```csharp
dynamic cmd = new Cmd();
dynamic posh = new Powershell();
cmd.dir();
posh.ls();
```

`cmd.dir()` is equivalent to `cmd /c dir`

**Etimology**

> The name is almost the same than cmd, but is prefixed with shared meaning that any person can extend it by adding it's own cli

# How to add your own CLI

1. First you need to add a class that extends the CommandoBase<T> abstract class or implement the ICommando interface and inherit from DynamicObject.

> This class will parse arguments and commands, and resolve when invoking, getting, indexing a member

2. Extends the ShellBase<T> abstract class or implement IRunner, ICommander<T> interface and inherit from DynamicObject

**A shell let's you interact with your operating system through CLI or GUI.**

> This class will execute the command on your cli.

3. Extends the Cli<T> abstract class.

> you just need to add your constructor to call the base one

**You can extend Argument or implement IArgument and change the way arguments are parsed**

The library is fully extensible (you can extend any point of it)
