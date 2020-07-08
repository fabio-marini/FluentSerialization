namespace FluentSerialization
{
    using FluentSerialization.Strategies;

    /// <summary>
    /// Provides fluent access to an instance of the specified type
    /// </summary>
    /// <typeparam name="T">The type of the underlying object</typeparam>
    public readonly struct TypeValue<T> : ITypeValue<T>
    {
        private readonly T typeInstance;
        private readonly IStrategies strategies;

        /// <summary>
        /// Create an instance of <see cref="TypeValue{T}"/> from an instance of the specified type
        /// </summary>
        /// <param name="typeInstance">The input instance</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        public TypeValue(T typeInstance, IStrategies strategies)
        {
            this.typeInstance = typeInstance;
            this.strategies = strategies;
        }

        /// <summary>
        /// Provides access to the underlying object
        /// </summary>
        public T Value { get => typeInstance; }

        /// <summary>
        /// Serialize the underlying byte array using the specified <see cref="ISerializationStrategy" />
        /// </summary>
        /// <returns>The <see cref="IStringValue" /> obtained after applying the <see cref="ISerializationStrategy" /></returns>
        public IStringValue Serialize()
        {
            var stg = strategies?.Serialization ?? new JsonSerializationStrategy();

            return new StringValue(stg.Serialize<T>(typeInstance), strategies);
        }
    }
}
