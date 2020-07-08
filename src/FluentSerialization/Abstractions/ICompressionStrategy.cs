namespace FluentSerialization
{
    /// <summary>
    /// Represents a compression strategy
    /// </summary>
    public interface ICompressionStrategy
    {
        /// <summary>
        /// Compress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to compress</param>
        /// <returns>The compressed byte array</returns>
        byte[] Compress(byte[] req);

        /// <summary>
        /// Decompress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decompress (obtained by calling <see cref="Compress(byte[])" />)</param>
        /// <returns>The decompressed byte array</returns>
        byte[] Decompress(byte[] req);
    }
}