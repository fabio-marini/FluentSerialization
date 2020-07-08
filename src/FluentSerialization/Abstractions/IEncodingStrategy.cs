namespace FluentSerialization
{
    /// <summary>
    /// Represents an encoding strategy
    /// </summary>
    public interface IEncodingStrategy
    {
        /// <summary>
        /// Decode the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decode (obtained by calling <see cref="Encode(string)" />)</param>
        /// <returns>The original string</returns>
        string Decode(byte[] req);

        /// <summary>
        /// Encode the specified string
        /// </summary>
        /// <param name="req">The string to encode</param>
        /// <returns>The input string, as an encoded byte array</returns>
        byte[] Encode(string req);
    }
}