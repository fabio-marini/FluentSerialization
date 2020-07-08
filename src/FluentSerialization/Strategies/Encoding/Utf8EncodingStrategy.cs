namespace FluentSerialization.Strategies
{
    using System.Text;

    /// <summary>
    /// UTF8 encoding strategy
    /// </summary>
    public class Utf8EncodingStrategy : IEncodingStrategy
    {
        /// <summary>
        /// Decode the specified byte array using the UTF8 encoding
        /// </summary>
        /// <param name="req">The byte array to decode (obtained by calling <see cref="Encode(string)" />)</param>
        /// <returns>The original string</returns>
        public string Decode(byte[] req) => Encoding.UTF8.GetString(req);

        /// <summary>
        /// Encode the specified string
        /// </summary>
        /// <param name="req">The string to encode</param>
        /// <returns>The input string, as an encoded byte array</returns>
        public byte[] Encode(string req) => Encoding.UTF8.GetBytes(req);
    }
}
