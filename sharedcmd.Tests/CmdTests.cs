using FakeItEasy;

using sharedcmd.Commands;
using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;
using sharedcmd.Runners.Shells;

using Xunit;

namespace sharedcmd.Tests
{
    public static class CmdTests
    {
        private static readonly dynamic cmd;

        private static readonly CmdShell shell;

        static CmdTests()
        {
            shell = A.Fake<CmdShell>();
            A.CallTo(() => shell.GiveOrder())
             .Returns(new CmdCommando(shell));
            cmd = new Cmd(shell);
        }

        [Fact]
        public static void ShouldBeAbleToBuildACommandAsProperty()
        {
            var commando = cmd.git;
            Assert.NotNull(commando);
        }

        [Fact]
        public static void ShouldCreateCommandWithRunner()
        {
            cmd.git();
            A.CallTo(() => shell.Run(A.Dummy<IRunOptions>()))
             .MustHaveHappenedOnceExactly();
        }
    }
}