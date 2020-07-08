namespace FluentSerialization.Strategies
{
    using Newtonsoft.Json;
    using System.IO;

    /// <summary>
    /// JSON serialization strategy
    /// </summary>
    public class JsonSerializationStrategy : ISerializationStrategy
    {
        private readonly JsonSerializer serializer;

        /// <summary>
        /// Create an instance of the <see cref="JsonSerializationStrategy"/>
        /// </summary>
        /// <param name="settings">The optional <see cref="JsonSerializerSettings"/> to use</param>
        public JsonSerializationStrategy(JsonSerializerSettings settings = null)
        {
            serializer = (settings == null) ? JsonSerializer.Create() : JsonSerializer.Create(settings);
        }

        /// <summary>
        /// Deserialize the specified object using <see cref="JsonConvert"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize</typeparam>
        /// <param name="req">A string representation of the specified object (obtained by calling <see cref="Serialize{T}(T)" />)</param>
        /// <returns>The deserialized object</returns>
        public T Deserialize<T>(string req)
        {
            var sr = new StringReader(req);
            
            return (T)serializer.Deserialize(sr, typeof(T));
        }

        /// <summary>
        /// Serialize the specified object using <see cref="JsonConvert"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="req">The object to serialize</param>
        /// <returns>A string representation of the specified object</returns>
        public string Serialize<T>(T req)
        {
            var sw = new StringWriter();
            serializer.Serialize(sw, req);

            return sw.ToString();
        }
    }
}
