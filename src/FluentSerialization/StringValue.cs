namespace FluentSerialization
{
    using FluentSerialization.Strategies;
    using System;

    /// <summary>
    /// Provides fluent access to a string
    /// </summary>
    public readonly struct StringValue : IStringValue
    {
        private readonly string stringValue;
        private readonly IStrategies strategies;

        /// <summary>
        /// Create an instance of <see cref="StringValue"/> from the specified string
        /// </summary>
        /// <param name="stringValue">The input string</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        public StringValue(string stringValue, IStrategies strategies)
        {
            this.stringValue = stringValue;
            this.strategies = strategies;
        }

        /// <summary>
        /// Provides access to the underlying string
        /// </summary>
        public String Value { get => stringValue; }

        /// <summary>
        /// Deserialize the underlying byte array using the specified <see cref="ISerializationStrategy" />
        /// </summary>
        /// <returns>The <see cref="ITypeValue{T}" /> obtained after applying the <see cref="ISerializationStrategy" /></returns>
        public ITypeValue<T> Deserialize<T>()
        {
            var stg = strategies?.Serialization ?? new JsonSerializationStrategy();

            return new TypeValue<T>(stg.Deserialize<T>(stringValue), strategies);
        }

        /// <summary>
        /// Encode the underlying byte array using the specified <see cref="IEncodingStrategy" />
        /// </summary>
        /// <returns>The <see cref="IBinaryValue" /> obtained after applying the <see cref="IEncodingStrategy" /></returns>
        public IBinaryValue Encode()
        {
            var stg = strategies?.Encoding ?? new Utf8EncodingStrategy();

            return new BinaryValue(stg.Encode(stringValue), strategies);
        }

        /// <summary>
        /// Convert the underlying string to its corresponding Base64 byte array
        /// </summary>
        /// <returns>A Base64-encoded byte array representation of the underlying string</returns>
        public IBinaryValue FromBase64()
        {
            return new BinaryValue(Convert.FromBase64String(stringValue), strategies);
        }
    }
}
