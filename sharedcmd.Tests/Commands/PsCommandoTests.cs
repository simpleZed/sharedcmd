using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests.Commands
{
    public sealed class PsCommandoTests
    {
        private PsShell shell;

        private dynamic powershell;

        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<PsShell>();
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));
            powershell = new Powershell(shell);
        }

        [Test]
        public void ShouldBeAbleToBuildACommand()
        {
            var commando = powershell.git;
            Assert.NotNull(commando);
            Assert.IsInstanceOf<PsCommando>(commando);
        }

        [Test]
        public void ShouldBeAbleToRunACommand()
        {
            powershell.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToRunACommandByIndex()
        {
            powershell["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToGetOutputFromCommand()
        {
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("out");
            var result = powershell.git();
            Assert.AreEqual("out", result);
        }

        [Test]
        public void ShouldBeAbleToBuildSubCommands()
        {
            var command = powershell.git.clone;
            Assert.NotNull(command);
            Assert.IsInstanceOf<PsCommando>(command);
        }

        [Test]
        public void ShouldBeAbleToBuildSubCommandsByIndex()
        {
            var command = powershell.git["clone"];
            Assert.NotNull(command);
            Assert.IsInstanceOf<PsCommando>(command);
        }

        [Test]
        public void ShouldBeAbleToRunWithSubCommand()
        {
            IRunOptions expectedRunOptions = null;
            powershell.git.clone();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git clone", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithSubCommandByIndex()
        {
            IRunOptions expectedRunOptions = null;
            powershell.git["clone"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git clone", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnCommand()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git(help: "--");
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git --help", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnCommandByIndex()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git["--help"]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git --help", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git.log["--grep"]("test");
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex2()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git.log["--grep"]["test"]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByIndex3()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git.log["--grep", "test"]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsAndValuesOnCommandByName()
        {
            const string Argument = "--help";
            IRunOptions expectedRunOptions = null;
            powershell.git.log("test", grep: "--");
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);

            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual("git log --grep test", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommand()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            powershell.git.clone(Argument);
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual($"git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            powershell.git.clone[Argument]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual($"git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex2()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            powershell.git["clone"][Argument]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual($"git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToRunWithArgumentsOnSubCommandByIndex3()
        {
            const string Argument = "https://github.com/manojlds/cmd";
            IRunOptions expectedRunOptions = null;
            powershell.git["clone", Argument]();
            A.CallTo(() => shell.BuildArgument(An<Argument>.That.IsNotNull()))
             .Returns(Argument);
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .WhenArgumentsMatch(a =>
             {
                 expectedRunOptions = a.Get<IRunOptions>(0);
                 return expectedRunOptions is not null;
             })
             .MustHaveHappened();
            Assert.NotNull(expectedRunOptions);
            Assert.AreEqual("powershell", expectedRunOptions.Command);
            Assert.AreEqual($"git clone {Argument}", expectedRunOptions.Arguments);
        }

        [Test]
        public void ShouldBeAbleToCallMultipleCommandsWithPreBuiltCommando()
        {
            IRunOptions branchRunOptions = null;
            var git = powershell.git;
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
