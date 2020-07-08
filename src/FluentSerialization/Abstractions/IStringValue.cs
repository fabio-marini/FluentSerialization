namespace FluentSerialization
{
    /// <summary>
    /// Provides fluent access to a string
    /// </summary>
    public interface IStringValue
    {
        /// <summary>
        /// Convert the underlying string to its corresponding Base64 byte array
        /// </summary>
        /// <returns>A Base64-encoded byte array representation of the underlying string</returns>
        IBinaryValue FromBase64();

        /// <summary>
        /// Encode the underlying byte array using the specified <see cref="IEncodingStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncodingStrategy" /></returns>
        IBinaryValue Encode();

        /// <summary>
        /// Deserialize the underlying byte array using the specified <see cref="ISerializationStrategy" />
        /// </summary>
        /// <returns>The <see cref="ITypeValue{T}" /> obtained after applying the <see cref="ISerializationStrategy" /></returns>
        ITypeValue<T> Deserialize<T>();

        /// <summary>
        /// Provides access to the underlying string
        /// </summary>
        string Value { get; }
    }
}
