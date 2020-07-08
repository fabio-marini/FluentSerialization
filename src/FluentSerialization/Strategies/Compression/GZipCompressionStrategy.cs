namespace FluentSerialization.Strategies
{
    using ICSharpCode.SharpZipLib.GZip;
    using System.IO;

    /// <summary>
    /// GZip compression strategy
    /// </summary>
    public class GZipCompressionStrategy : ICompressionStrategy
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

            GZip.Compress(sourceStream, targetStream, true, level: 9);

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

            GZip.Decompress(sourceStream, targetStream, true);

            return targetStream.ToArray();
        }
    }
}
