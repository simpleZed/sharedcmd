using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests.Runners.Shell
{
    public sealed class CmdShellTests
    {
        private dynamic cmd;

        private CmdShell shell;

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
        public void ShouldInvokeACmdlet()
        {
            cmd.Write.Host();
        }

        [Test]
        public void ShouldInvokeACmdletByIndex()
        {
            cmd.Write["Host"]();
        }

        [Test]
        public void ShouldInvokeACmdletWithArgument()
        {
            cmd.Write.Host("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithArgumentByIndex()
        {
            cmd.Write["Host"]("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithParameter()
        {
            cmd.Write.Host("test", Object: "--");
        }

        [Test]
        public void ShouldInvokeACmdletWithParameterByIndex()
        {
            cmd.Write.Host["--Object"]("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithSwitchFlag()
        {
            cmd.Write.Host(NoNewLine: "--");
        }

        [Test]
        public void ShouldInvokeACmdletWithSwitchFlagByIndex()
        {
            cmd.Write.Host["--NoNewLine"]();
        }
    }
}