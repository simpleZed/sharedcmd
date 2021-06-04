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
        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<BashShell>();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("result");

            A.CallTo(() => shell.GiveOrder())
             .Returns(new BashCommando(shell));
            bash = new Bash(shell);
        }

        [Test]
        public void ShouldBeAbleToCallArbitraryCommandOnCmd()
        {
            bash.git();
        }

        [Test]
        public void ShouldBeAbleToCallArbitrarySubCommand()
        {
            _ = bash.git.clone;
        }

        [Test]
        public void ShouldBeAbleToExecuteWithSubCommand()
        {
            bash.git.Clone();
        }

        [Test]
        public void ShouldBeAbleToPassArgumentsToACommand()
        {
            bash.Git.Clone("http://github.com/manojlds/cmd");
        }

        [Test]
        public void ShouldBeAbleToPassFlags()
        {
            bash.Git.Pull(r: true);
        }

        [Test]
        public void ShouldBeAbleToPassArgumentstoFlags()
        {
            bash.Git.Checkout(b: "master");
        }

        [Test]
        public void ShouldBeAbleToPreBuildACommandAndThenExecuteIt()
        {
            var git = bash.git;
            git.Clone();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandOnCmd()
        {
            _ = bash.git;
            _ = bash.svn;
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandOnCmd()
        {
            bash.git();
            bash.svn();
        }

        [Test]
        public void ShouldBeAbleToChooseADifferentShell()
        {
            dynamic bash = new Powershell();
            Assert.NotNull(bash);
            Assert.AreNotEqual(this.bash, bash);
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariables()
        {
            bash._Env(new Dictionary<string, string> { { "PATH", @"C:\" } });
        }
    }
}
