namespace FluentSerialization.Strategies
{
    using Newtonsoft.Json;
    using System.Xml.Linq;

    /// <summary>
    /// XML serialization strategy (using <see cref="JsonSerializationStrategy"/>)
    /// </summary>
    public class XmlSerializationStrategy : ISerializationStrategy
    {
        private readonly JsonSerializationStrategy jsonStrategy;

        /// <summary>
        /// Create an instance of the <see cref="XmlSerializationStrategy"/>
        /// </summary>
        /// <param name="settings">The optional <see cref="JsonSerializerSettings"/> to use</param>
        public XmlSerializationStrategy(JsonSerializerSettings settings = null)
        {
            jsonStrategy = new JsonSerializationStrategy(settings);
        }

        /// <summary>
        /// Deserialize the specified object using the <see cref="JsonSerializationStrategy"/> internally
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize</typeparam>
        /// <param name="req">A string representation of the specified object (obtained by calling <see cref="Serialize{T}(T)" />)</param>
        /// <returns>The deserialized object</returns>
        public T Deserialize<T>(string req)
        {
            // convert the XML string to a JSON string first, then deserialize using the JSON serialization strategy
            var doc = XDocument.Parse(req);

            var json = JsonConvert.SerializeXNode(doc, Formatting.None, omitRootObject: true);

            return jsonStrategy.Deserialize<T>(json);
        }

        /// <summary>
        /// Serialize the specified object using the <see cref="JsonSerializationStrategy"/> internally
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="req">The object to serialize</param>
        /// <returns>A string representation of the specified object</returns>
        public string Serialize<T>(T req)
        {
            // serialize using the JSON serialization strategy first, then convert the resulting JSON string to XML
            var json = jsonStrategy.Serialize<T>(req);

            var doc = JsonConvert.DeserializeXNode(json, "Root", writeArrayAttribute: true, encodeSpecialCharacters: true);

            return doc.ToString(SaveOptions.DisableFormatting);
        }
    }
}
