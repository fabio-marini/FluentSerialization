namespace FluentSerialization.Strategies
{
    using ICSharpCode.SharpZipLib.BZip2;
    using System.IO;

    /// <summary>
    /// BZip2 compression strategy
    /// </summary>
    public class BZip2CompressionStrategy : ICompressionStrategy
    {
        /// <summary>
        /// Compress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to compress</param>
        /// <returns>The compressed byte array</returns>
        public byte[] Compress(byte[] req)
        {
            var sourceStream = new MemoryStream(req);
            var targetStream = new MemoryStream();

            BZip2.Compress(sourceStream, targetStream, true, 9);

            return targetStream.ToArray();
        }

        /// <summary>
        /// Decompress the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decompress (obtained by calling <see cref="Compress(byte[])" />)</param>
        /// <returns>The decompressed byte array</returns>
        public byte[] Decompress(byte[] req)
        {
            var sourceStream = new MemoryStream(req);
            var targetStream = new MemoryStream();

            BZip2.Decompress(sourceStream, targetStream, true);

            return targetStream.ToArray();
        }
    }
}
