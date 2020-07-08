namespace FluentSerialization
{
    /// <summary>
    /// Represents a serialization strategy
    /// </summary>
    public interface ISerializationStrategy
    {
        /// <summary>
        /// Serialize the specified object
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="req">The object to serialize</param>
        /// <returns>A string representation of the specified object</returns>
        string Serialize<T>(T req);

        /// <summary>
        /// Deserialize the specified object
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize</typeparam>
        /// <param name="req">A string representation of the specified object (obtained by calling <see cref="Serialize{T}(T)" />)</param>
        /// <returns>The deserialized object</returns>
        T Deserialize<T>(string req);
    }
}