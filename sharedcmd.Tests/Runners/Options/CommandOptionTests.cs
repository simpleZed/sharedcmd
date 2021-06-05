using NUnit.Framework;

using sharedcmd.Runners;
using sharedcmd.Runners.Options;

namespace sharedcmd.Tests.Runners.Options
{
    public sealed class CommandOptionTests
    {
        [Test]
        public void ShouldPrefixAGivenFlagWithASingleHiphen()
        {
            var argument = CommandOptionFactory.Of<CommandOption>("-", "f");
            Assert.AreEqual("-f", argument.ToString());
        }

        [Test]
        public void ShouldPrefixAGivenFlagWithADoubleHiphen()
        {
            var argument = CommandOptionFactory.Of<CommandOption>("--", "f1");
            Assert.AreEqual("--f1", argument.ToString());
        }

        [Test]
        public void ShouldPrefixAGivenFlagWithAnySymbol()
        {
            var argument = CommandOptionFactory.Of<CommandOption>("@", "f1");
            Assert.AreEqual("@f1", argument.ToString());
        }

        [Test]
        public void ShouldOnlyUseFlagAsValueIfNoPrefixSpecifiedForArgument()
        {
            var argument = CommandOptionFactory.Of<CommandOption>(null!, "value");
            Assert.AreEqual("value", argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldIgnoreFlagIfItsNotString()
        {
            var argument = CommandOptionFactory.Of<CommandOption>("--", true);
            Assert.AreEqual("--", argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldBeNullIfPrefixAndFlagAreNull()
        {
            var argument = CommandOptionFactory.Of<CommandOption>(null!, null!);
            Assert.Null(argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldBeNullIfPrefixIsNullAndFlagIsNotString()
        {
            var argument = CommandOptionFactory.Of<CommandOption>(null!, true);
            Assert.Null(argument.ToString());
        }
    }
}
