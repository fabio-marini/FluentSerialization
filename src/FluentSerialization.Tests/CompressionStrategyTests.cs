namespace FluentSerialization.Tests.Compression
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class CompressionStrategyTests
    {
        string longText = Enumerable.Range(1, 128).Select(i => $"Hello world {i}! :)").Aggregate((s1, s2) => $"{s1}\r\n{s2}");

        [TestMethod]
        public void TestPassThruCompressionStrategy()
        {
            var strategy = new PassThruCompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes("Hello World!");
            var compressResponse = strategy.Compress(compressRequest);

            compressResponse.SequenceEqual(compressRequest).Should().BeTrue();

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.SequenceEqual(decompressRequest).Should().BeTrue();
        }

        [TestMethod]
        public void TestGZipCompressionStrategy()
        {
            var strategy = new GZipCompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes(longText);
            var compressResponse = strategy.Compress(compressRequest);

            compressRequest.Length.Should().Be(2578);
            compressResponse.Length.Should().Be(325);

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.Length.Should().Be(2578);
        }

        [TestMethod]
        public void TestBZip2CompressionStrategy()
        {
            var strategy = new BZip2CompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes(longText);
            var compressResponse = strategy.Compress(compressRequest);

            compressRequest.Length.Should().Be(2578);
            compressResponse.Length.Should().Be(223);

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.Length.Should().Be(2578);
        }
    }
}
