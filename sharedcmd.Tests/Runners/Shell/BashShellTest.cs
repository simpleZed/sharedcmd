using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests.Runners.Shell
{
    public sealed class BashShellTest
    {
        private dynamic bash;

        private BashShell shell;

        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<BashShell>();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("result");

            A.CallTo(() => shell.GenerateCommand())
             .Returns(new BashCommando(shell));
            bash = new Bash(shell);
        }

        [Test]
        public void ShouldInvokeALinuxCommand()
        {
            bash.nano.historylog();
        }

        [Test]
        public void ShouldInvokeALinuxByIndex()
        {
            bash.nano["--historylog"]();
        }

        [Test]
        public void ShouldInvokeALinuxWithArgument()
        {
            bash.nano("test");
        }

        [Test]
        public void ShouldInvokeALinuxWithArgumentByIndex()
        {
            bash.nano["-T 4"]("test");
        }

        [Test]
        public void ShouldInvokeALinuxWithParameter()
        {
            bash.nano("test", G: "-");
        }

        [Test]
        public void ShouldInvokeALinuxWithParameterByIndex()
        {
            bash.nano["-G"]("test");
        }

        [Test]
        public void ShouldInvokeALinuxWithSwitchFlag()
        {
            bash.nano(boldtext: "--");
        }

        [Test]
        public void ShouldInvokeALinuxWithSwitchFlagByIndex()
        {
            bash.nano["--boldtext"]();
        }
    }
}