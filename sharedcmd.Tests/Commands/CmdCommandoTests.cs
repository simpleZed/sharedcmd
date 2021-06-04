using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests.Commands
{
    public sealed class CmdCommandoTests
    {
        private CmdShell shell;

        private dynamic cmd;

        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<CmdShell>();
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));
            cmd = new Cmd(shell);
        }

        [Test]
        public void ShouldBeAbleToBuildACommand()
        {
            var commando = cmd.git;
            Assert.NotNull(commando);
            Assert.IsInstanceOf<CmdCommando>(commando);
        }

        [Test]
        public void ShouldBeAbleToRunACommand()
        {
            cmd.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToRunACommandByIndex()
        {
            cmd["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToGetOutputFromCommand()
        {
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("out");
            var result = cmd.git();
            Assert.AreEqual("out", result);
        }

        [Test]
        public void ShouldBeAbleToBuildSubCommands()
        {
            var command = cmd.git.clone;
            Assert.NotNull(command);
            Assert.IsInstanceOf<CmdCommando>(command);
        }

        [Test]
        public void ShouldBeAbleToBuildSubCommandsByIndex()
        {
            var command = cmd.git["clone"];
            Assert.NotNull(command);
            Assert.IsInstanceOf<CmdCommando>(command);
        }

        [Test]
        public void ShouldBeAbleToRunWithSubCommand()
        {
            IRunOptions expectedRunOptions = null;
            cmd.git.clone();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git clone", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithSubCommandByIndex()
        {
            IRunOptions expectedRunOptions = null;
            cmd.git["clone"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git clone", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnCommand()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git(help: "--");
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git --help", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnCommandByIndex()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git["--help"]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git --help", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git.log["--grep"]("test");
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex2()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git.log["--grep"]["test"]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex3()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git.log["--grep", "test"]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByName()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            cmd.git.log("test", grep: "--");
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual("/c git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommand()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            cmd.git.clone(Argument);
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual($"/c git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            cmd.git.clone[Argument]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual($"/c git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex2()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            cmd.git["clone"][Argument]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual($"/c git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex3()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            cmd.git["clone", Argument]();
            A.CallTo(() => shell.BuildArgument(An<CommandOption>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("cmd", expectedRunOptions.Command);
            Assert.AreEqual($"/c git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToCallMultipleCommandsWithPreBuiltCommando()
        {
            IRunOptions branchRunOptions = null;
            var git = cmd.git;
            git.Clone();
            git.branch(async: true);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(options => options.Arguments.StartsWith("branch"))))
             .WhenArgumentsMatch(a =>
             {
                 branchRunOptions = a.Get<IRunOptions>(0);
                 return branchRunOptions is not null;
             })
             .MustHaveHappened();

            IRunOptions cloneRunOptions = null;
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(options => options.Arguments.StartsWith("clone"))))
             .WhenArgumentsMatch(a =>
             {
                 cloneRunOptions = a.Get<IRunOptions>(0);
                 return cloneRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(branchRunOptions);
            Assert.NotNull(cloneRunOptions);
        }
    }
}