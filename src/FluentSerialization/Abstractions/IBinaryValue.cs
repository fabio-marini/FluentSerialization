namespace FluentSerialization
{
    /// <summary>
    /// Provides fluent access to a byte array
    /// </summary>
    public interface IBinaryValue
    {
        /// <summary>
        /// Convert the underlying byte array to its corresponding Base64 string
        /// </summary>
        /// <returns>A Base64-encoded string representation of the underlying byte array</returns>
        IStringValue ToBase64();

        /// <summary>
        /// Decode the underlying byte array using the specified <see cref="IEncodingStrategy" />
        /// </summary>
        /// <returns>The <see cref="IStringValue" /> obtained after applying the <see cref="IEncodingStrategy" /></returns>
        IStringValue Decode();

        /// <summary>
        /// Compress the underlying byte array using the specified <see cref="ICompressionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="ICompressionStrategy" /></returns>
        IBinaryValue Compress();

        /// <summary>
        /// Decompress the underlying byte array using the specified <see cref="ICompressionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="ICompressionStrategy" /></returns>
        IBinaryValue Decompress();

        /// <summary>
        /// Encrypt the underlying byte array using the specified <see cref="IEncryptionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncryptionStrategy" /></returns>
        IBinaryValue Encrypt();

        /// <summary>
        /// Decrypt the underlying byte array using the specified <see cref="IEncryptionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncryptionStrategy" /></returns>
        IBinaryValue Decrypt();

        /// <summary>
        /// Provides access to the underlying byte array
        /// </summary>
        byte[] Value { get; }
    }
}
