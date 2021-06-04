using sharedcmd.Runners.Arguments;

namespace sharedcmd.Runners
{
    /// <summary>
    /// Factory methods for the <see cref="CommandOption"/> class and it's derived types.
    /// </summary>
    public static class ArgumentFactory
    {
        /// <summary>
        /// Generates a new <see cref="CommandOption"/> instance.
        /// </summary>
        /// <typeparam name="T">
        /// Type of argument to generate.
        /// </typeparam>
        /// <param name="prefix">
        /// Prefix to use when using a flag, can be the value to use if no flag is provided.
        /// </param>
        /// <param name="flag">
        /// The cli option to use.
        /// </param>
        /// <returns>
        /// The new instance of <see cref="CommandOption"/> with it's state configured.
        /// </returns>
        public static T Of<T>(string prefix, object flag) where T : CommandOption, new()
        {
            return new()
            {
                Prefix = prefix,
                Flag = flag is string f ? f : null!
            };
        }
    }
}