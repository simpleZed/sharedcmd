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
        [SetUp]
        public void SetUp()
        {
           shell = A.Fake<CmdShell>();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("result");

            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));
            cmd = new Cmd(shell);
        }

        [Test]
        public void ShouldBeAbleToCallArbitraryCommandOnCmd()
        {
            cmd.git();
        }

        [Test]
        public void ShouldBeAbleToCallArbitrarySubCommand()
        {
            _ = cmd.git.clone;
        }

        [Test]
        public void ShouldBeAbleToExecuteWithSubCommand()
        {
            cmd.git.Clone();
        }

        [Test]
        public void ShouldBeAbleToPassArgumentsToACommand()
        {
            cmd.Git.Clone("http://github.com/manojlds/cmd");
        }

        [Test]
        public void ShouldBeAbleToPassFlags()
        {
            cmd.Git.Pull(r: true);
        }

        [Test]
        public void ShouldBeAbleToPassArgumentstoFlags()
        {
            cmd.Git.Checkout(b: "master");
        }

        [Test]
        public void ShouldBeAbleToPreBuildACommandAndThenExecuteIt()
        {
            var git = cmd.git;
            git.Clone();
        }

        [Test]
        public void ShouldBeAbleToBuildMultipleCommandOnCmd()
        {
            _ = cmd.git;
            _ = cmd.svn;
        }

        [Test]
        public void ShouldBeAbleToRunMultipleCommandOnCmd()
        {
            cmd.git();
            cmd.svn();
        }

        [Test]
        public void ShouldBeAbleToChooseADifferentShell()
        {
            dynamic cmd = new Cmd();
            Assert.NotNull(cmd);
            Assert.AreNotEqual(this.cmd, cmd);
        }

        [Test]
        public void ShouldBeAbleToSetEnvironmentVariables()
        {
            cmd._Env(new Dictionary<string, string> { { "PATH", @"C:\" } });
        }
    }
}