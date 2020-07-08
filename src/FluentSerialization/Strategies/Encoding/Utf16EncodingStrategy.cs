namespace FluentSerialization.Strategies
{
    using System.Text;

    /// <summary>
    /// UTF16 encoding strategy
    /// </summary>
    public class Utf16EncodingStrategy : IEncodingStrategy
    {
        /// <summary>
        /// Decode the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decode (obtained by calling <see cref="Encode(string)" />)</param>
        /// <returns>The original string</returns>
        public string Decode(byte[] req) => Encoding.Unicode.GetString(req);

        /// <summary>
        /// Encode the specified string
        /// </summary>
        /// <param name="req">The string to encode</param>
        /// <returns>The input string, as an encoded byte array</returns>
        public byte[] Encode(string req) => Encoding.Unicode.GetBytes(req);
    }
}
