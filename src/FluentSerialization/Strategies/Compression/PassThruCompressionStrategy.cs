namespace FluentSerialization.Strategies
{
    /// <summary>
    /// Pass thru compression strategy
    /// </summary>
    public class PassThruCompressionStrategy : ICompressionStrategy
    {
        /// <summary>
        /// Compress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to compress</param>
        /// <returns>The compressed byte array</returns>
        public byte[] Compress(byte[] req) => req;

        /// <summary>
        /// Decompress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decompress (obtained by calling <see cref="Compress(byte[])" />)</param>
        /// <returns>The decompressed byte array</returns>
        public byte[] Decompress(byte[] req) => req;
    }
}
