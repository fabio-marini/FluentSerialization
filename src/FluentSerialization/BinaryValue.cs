namespace FluentSerialization
{
    using FluentSerialization.Strategies;
    using System;

    /// <summary>
    /// Provides fluent access to a byte array
    /// </summary>
    public readonly struct BinaryValue : IBinaryValue
    {
        private readonly byte[] binaryValue;
        private readonly IStrategies strategies;

        /// <summary>
        /// Create an instance of <see cref="BinaryValue"/> from the specified byte array
        /// </summary>
        /// <param name="binaryValue">The input byte array</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        public BinaryValue(byte[] binaryValue, IStrategies strategies)
        {
            this.binaryValue = binaryValue;
            this.strategies = strategies;
        }

        /// <summary>
        /// Provides access to the underlying byte array
        /// </summary>
        public byte[] Value { get => binaryValue; }

        /// <summary>
        /// Compress the underlying byte array using the specified <see cref="ICompressionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="ICompressionStrategy" /></returns>
        public IBinaryValue Compress()
        {
            var stg = strategies?.Compression ?? new PassThruCompressionStrategy();

            return new BinaryValue(stg.Compress(binaryValue), strategies);
        }

        /// <summary>
        /// Decode the underlying byte array using the specified <see cref="IEncodingStrategy" />
        /// </summary>
        /// <returns>The <see cref="IStringValue" /> obtained after applying the <see cref="IEncodingStrategy" /></returns>
        public IStringValue Decode()
        {
            var stg = strategies?.Encoding ?? new Utf8EncodingStrategy();

            return new StringValue(stg.Decode(binaryValue), strategies);
        }

        /// <summary>
        /// Decompress the underlying byte array using the specified <see cref="ICompressionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="ICompressionStrategy" /></returns>
        public IBinaryValue Decompress()
        {
            var stg = strategies?.Compression ?? new PassThruCompressionStrategy();

            return new BinaryValue(stg.Decompress(binaryValue), strategies);
        }

        /// <summary>
        /// Decrypt the underlying byte array using the specified <see cref="IEncryptionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncryptionStrategy" /></returns>
        public IBinaryValue Decrypt()
        {
            var stg = strategies?.Encryption ?? new PassThruEncryptionStrategy();

            return new BinaryValue(stg.Decrypt(binaryValue), strategies);
        }

        /// <summary>
        /// Encrypt the underlying byte array using the specified <see cref="IEncryptionStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncryptionStrategy" /></returns>
        public IBinaryValue Encrypt()
        {
            var stg = strategies?.Encryption ?? new PassThruEncryptionStrategy();

            return new BinaryValue(stg.Encrypt(binaryValue), strategies);
        }

        /// <summary>
        /// Convert the underlying byte array to its corresponding Base64 string
        /// </summary>
        /// <returns>A Base64-encoded string representation of the underlying byte array</returns>
        public IStringValue ToBase64()
        {
            return new StringValue(Convert.ToBase64String(binaryValue), strategies);
        }
    }
}
