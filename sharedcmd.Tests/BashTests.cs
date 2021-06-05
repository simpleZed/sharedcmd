using System.Collections.Generic;

using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests
{
    public sealed partial class BashTests
    {
        private dynamic bash;

        private BashShell shell;

        [SetUp]
        public void Setup()
        {
            shell = A.Fake<BashShell>();
            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));
            bash = new Bash(shell);
        }

        [Test]
        public void ShouldGetCommandoWhenAccessingMember()
        {
            var commando = bash.git;
            Assert.NotNull(commando);
            Assert.IsInstanceOf<BashCommando>(commando);
        }

        [Test]
        public void ShouldGetCommandoWhenIndexingMember()
        {
            var commando = bash["git"];
            Assert.NotNull(commando);
            Assert.IsInstanceOf<BashCommando>(commando);
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingMember()
        {
            bash.git();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldCreateCommandWithRunnerWhenInvokingIndex()
        {
            bash["git"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            dynamic ls = bash.ls;
            Assert.NotNull(ls);
            Assert.IsInstanceOf<BashCommando>(ls);

            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            dynamic dir = bash.dir;
            Assert.NotNull(dir);
            Assert.IsInstanceOf<BashCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            dynamic ls = bash["git"];
            Assert.NotNull(ls);
            Assert.IsInstanceOf<BashCommando>(ls);

            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            dynamic dir = bash["dir"];
            Assert.NotNull(dir);
            Assert.IsInstanceOf<BashCommando>(dir);
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmd()
        {
            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            bash.ls();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "-c ls")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            bash.dir();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "-c dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandsOnCmdIndex()
        {
            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            bash["ls"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "-c ls")))
             .MustHaveHappenedOnceExactly();

            A.CallTo(() => shell.FindCommand())
             .Returns(new BashCommando(shell));

            bash["dir"]();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.Matches(r => r.Arguments == "-c dir")))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariablesOnCmd()
        {
            bash._Env(("PATH", @"C:\"));
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
            bash._Env(environmentVariables);
            bash._Env(emptyVars);
            A.CallToSet(() => shell.EnvironmentVariables)
             .MustHaveHappened();
            Assert.NotNull(shell.EnvironmentVariables);
            Assert.AreEqual(1, environmentVariables.Count);
        }
    }
}
