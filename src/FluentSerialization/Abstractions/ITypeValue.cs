namespace FluentSerialization
{
    /// <summary>
    /// Provides fluent access to an instance of the specified type
    /// </summary>
    /// <typeparam name="T">The type of the underlying object</typeparam>
    public interface ITypeValue<T>
    {
        /// <summary>
        /// Serialize the underlying byte array using the specified <see cref="ISerializationStrategy" />
        /// </summary>
        /// <returns>The <see cref="IStringValue" /> obtained after applying the <see cref="ISerializationStrategy" /></returns>
        IStringValue Serialize();

        /// <summary>
        /// Provides access to the underlying object
        /// </summary>
        T Value { get; }
    }
}
