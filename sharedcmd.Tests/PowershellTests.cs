using System.Collections.Generic;

using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests
{
    public sealed partial class PowershellTests
    {
        private dynamic ps;

        private PsShell shell;

        [SetUp]
        public void Setup()
        {
            shell = A.Fake<PsShell>();
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));
            ps = new Powershell(shell);
        }

        [Test]
        public void ShouldGetCommandoWhenAccessingMember()
        {
            var commando = ps.git;
            Assert.NotNull(commando);
            Assert.IsInstanceOf<PsCommando>(commando);
        }

        [Test]
        public void ShouldGetCommandoWhenIndexingMember()
        {
            var commando = ps["git"];
            Assert.NotNull(commando);
            Assert.IsInstanceOf<PsCommando>(commando);
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingMember()
        {
            ps.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingIndex()
        {
            ps["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            dynamic ls = ps.ls;
            Assert.NotNull(ls);
            Assert.IsInstanceOf<PsCommando>(ls);

            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            dynamic dir = ps.dir;
            Assert.NotNull(dir);
            Assert.IsInstanceOf<PsCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            dynamic ls = ps["git"];
            Assert.NotNull(ls);
            Assert.IsInstanceOf<PsCommando>(ls);

            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            dynamic dir = ps["dir"];
            Assert.NotNull(dir);
            Assert.IsInstanceOf<PsCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            ps.ls();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "ls")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            ps.dir();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            ps["ls"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "ls")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.GiveOrder())
             .Returns(new PsCommando(shell));

            ps["dir"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariablesOnCmd()
        {
            var environmentVariables = new Dictionary<string, string>()
            {
                ["PATH"] = @"C:\"
            };
            ps._Env(environmentVariables);
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
            ps._Env(environmentVariables);
            ps._Env(emptyVars);
            A.CallToSet(() => shell.EnvironmentVariables)
             .MustHaveHappened();
            Assert.NotNull(shell.EnvironmentVariables);
            Assert.AreEqual(1, environmentVariables.Count);
        }
    }
}
