using FakeItEasy;

using NUnit.Framework;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Shells;

namespace sharedcmd.Tests.Runners.Shell
{
    public sealed class PsShellTests
    {
        private dynamic powershell;

        private PsShell shell;

        [SetUp]
        public void SetUp()
        {
            shell = A.Fake<PsShell>();
            A.CallTo(() => shell.Run(An<IRunOptions>.That.IsNotNull()))
             .Returns("result");

            A.CallTo(() => shell.FindCommand())
             .Returns(new PsCommando(shell));
            powershell = new Powershell(shell);
        }

        [Test]
        public void ShouldInvokeACmdlet()
        {
            powershell.Write.Host();
        }

        [Test]
        public void ShouldInvokeACmdletByIndex()
        {
            powershell.Write["Host"]();
        }

        [Test]
        public void ShouldInvokeACmdletWithArgument()
        {
            powershell.Write.Host("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithArgumentByIndex()
        {
            powershell.Write["Host"]("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithParameter()
        {
            powershell.Write.Host("test", Object: "--");
        }

        [Test]
        public void ShouldInvokeACmdletWithParameterByIndex()
        {
            powershell.Write.Host["--Object"]("test");
        }

        [Test]
        public void ShouldInvokeACmdletWithSwitchFlag()
        {
            powershell.Write.Host(NoNewLine: "--");
        }

        [Test]
        public void ShouldInvokeACmdletWithSwitchFlagByIndex()
        {
            powershell.Write.Host["--NoNewLine"]();
        }
    }
}