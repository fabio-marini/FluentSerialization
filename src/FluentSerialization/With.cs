namespace FluentSerialization
{
    /// <summary>
    /// Entry point for the FluentSerialization API
    /// </summary>
    public static class With
    {
        /// <summary>
        /// Create an instance of <see cref="StringValue"/> from the specified string
        /// </summary>
        /// <param name="stringValue">The input string</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        /// <returns>An instance of <see cref="StringValue"/></returns>
        public static IStringValue String(string stringValue, IStrategies strategies = null) => new StringValue(stringValue, strategies);

        /// <summary>
        /// Create an instance of <see cref="BinaryValue"/> from the specified byte array
        /// </summary>
        /// <param name="binaryValue">The input byte array</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        /// <returns>An instance of <see cref="BinaryValue"/></returns>
        public static IBinaryValue Bytes(byte[] binaryValue, IStrategies strategies = null) => new BinaryValue(binaryValue, strategies);

        /// <summary>
        /// Create an instance of <see cref="TypeValue{T}"/> from the specified object
        /// </summary>
        /// <param name="typeInstance">The input object</param>
        /// <param name="strategies">The <see cref="IStrategies"/> to use</param>
        /// <returns>An instance of <see cref="TypeValue{T}"/></returns>
        public static ITypeValue<T> Object<T>(T typeInstance, IStrategies strategies = null) => new TypeValue<T>(typeInstance, strategies);
    }
}
