using System.Collections.Generic;

using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests
{
    public sealed partial class CmdTests
    {
        private dynamic cmd;

        private CmdShell shell;

        [SetUp]
        public void Setup()
        {
            shell = A.Fake<CmdShell>();
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));
            cmd = new Cmd(shell);
        }

        [Test]
        public void ShouldGetCommandoWhenAccessingMember()
        {
            var commando = cmd.git;
            Assert.NotNull(commando);
            Assert.IsInstanceOf<CmdCommando>(commando);
        }

        [Test]
        public void ShouldGetCommandoWhenIndexingMember()
        {
            var commando = cmd["git"];
            Assert.NotNull(commando);
            Assert.IsInstanceOf<CmdCommando>(commando);
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingMember()
        {
            cmd.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingIndex()
        {
            cmd["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            dynamic ls = cmd.ls;
            Assert.NotNull(ls);
            Assert.IsInstanceOf<CmdCommando>(ls);

            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            dynamic dir = cmd.dir;
            Assert.NotNull(dir);
            Assert.IsInstanceOf<CmdCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            dynamic ls = cmd["git"];
            Assert.NotNull(ls);
            Assert.IsInstanceOf<CmdCommando>(ls);

            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            dynamic dir = cmd["dir"];
            Assert.NotNull(dir);
            Assert.IsInstanceOf<CmdCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            cmd.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "/c git")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            cmd.dir();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "/c dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            cmd["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "/c git")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));

            cmd["dir"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "/c dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariablesOnCmd()
        {
            cmd._Env(("PATH", @"C:\"));
            A.CallToSet(() => shell.EnvironmentVariables)
             .MustHaveHappened();

            Assert.AreEqual(shell.EnvironmentVariables, new Dictionary<string, string>()
            {
                ["PATH"] = @"C:\"
            });
        }

        [Test]
        public void ShouldNotBeAbleToClearEnvironmentVariablesOnCmd()
        {
            var environmentVariables = new Dictionary<string, string>()
            {
                ["PATH"] = @"C:\"
            };
            Dictionary<string, string> emptyVars = new();
            cmd._Env(environmentVariables);
            cmd._Env(emptyVars);
            A.CallToSet(() => shell.EnvironmentVariables)
             .MustHaveHappened();
            Assert.NotNull(shell.EnvironmentVariables);
            Assert.AreEqual(1, environmentVariables.Count);
        }
    }
}