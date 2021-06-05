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
        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<PsShell>();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("result");

            A.CallTo(() => shell.GenerateCommand())
             .Returns(new PsCommando(shell));
            ps = new Powershell(shell);
        }

        [Test]
        public void ShouldBeAbleToCallArbitraryCommandOnCmd()
        {
            ps.git();
        }

        [Test]
        public void ShouldBeAbleToCallArbitrarySubCommand()
        {
            _ = ps.git.clone;
        }

        [Test]
        public void ShouldBeAbleToExecuteWithSubCommand()
        {
            ps.git.Clone();
        }

        [Test]
        public void ShouldBeAbleToPassArgumentsToACommand()
        {
            ps.Git.Clone("http://github.com/manojlds/cmd");
        }

        [Test]
        public void ShouldBeAbleToPassFlags()
        {
            ps.Git.Pull(r: true);
        }

        [Test]
        public void ShouldBeAbleToPassArgumentstoFlags()
        {
            ps.Git.Checkout(b: "master");
        }

        [Test]
        public void ShouldBeAbleToPreBuildACommandAndThenExecuteIt()
        {
            var git = ps.git;
            git.Clone();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandOnCmd()
        {
            _ = ps.git;
            _ = ps.svn;
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandOnCmd()
        {
            ps.git();
            ps.svn();
        }

        [Test]
        public void ShouldBeAbleToChooseADifferentShell()
        {
            dynamic ps = new Powershell();
            Assert.NotNull(ps);
            Assert.AreNotEqual(this.ps, ps);
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariables()
        {
            ps._Env(new Dictionary<string, string> { { "PATH", @"C:\" } });
        }
    }
}