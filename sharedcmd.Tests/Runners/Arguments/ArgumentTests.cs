using NUnit.Framework;

using sharedcmd.Runners;
using sharedcmd.Runners.Arguments;

namespace sharedcmd.Tests.Runners.Arguments
{
    public sealed class ArgumentTests
    {
        [Test]
        public void ShouldPrefixAGivenFlagWithASingleHiphen()
        {
            var argument = ArgumentFactory.Of<Argument>("-", "f");
            Assert.AreEqual("-f", argument.ToString());
        }

        [Test]
        public void ShouldPrefixAGivenFlagWithADoubleHiphen()
        {
            var argument = ArgumentFactory.Of<Argument>("--", "f1");
            Assert.AreEqual("--f1", argument.ToString());
        }

        [Test]
        public void ShouldPrefixAGivenFlagWithAnySymbol()
        {
            var argument = ArgumentFactory.Of<Argument>("@", "f1");
            Assert.AreEqual("@f1", argument.ToString());
        }

        [Test]
        public void ShouldOnlyUseFlagAsValueIfNoPrefixSpecifiedForArgument()
        {
            var argument = ArgumentFactory.Of<Argument>(null!, "value");
            Assert.AreEqual("value", argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldIgnoreFlagIfItsNotString()
        {
            var argument = ArgumentFactory.Of<Argument>("--", true);
            Assert.AreEqual("--", argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldBeNullIfPrefixAndFlagAreNull()
        {
            var argument = ArgumentFactory.Of<Argument>(null!, null!);
            Assert.Null(argument.ToString());
        }

        [Test(Description = "This is not part of a normal flow, since it won't execute any command")]
        public void ShouldBeNullIfPrefixIsNullAndFlagIsNotString()
        {
            var argument = ArgumentFactory.Of<Argument>(null!, true);
            Assert.Null(argument.ToString());
        }
    }
}
