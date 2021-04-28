namespace FluentSerialization.Tests
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using System;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class EncodingStrategyTests
    {
        [Fact]
        public void TestUtf8EncodingStrategy()
        {
            var strategy = new Utf8EncodingStrategy();

            var encodeRequest = "Hello World!";
            var encodeResponse = strategy.Encode(encodeRequest);
            var expectedResponse = Encoding.UTF8.GetBytes(encodeRequest);

            encodeResponse.SequenceEqual(expectedResponse).Should().BeTrue();

            var decodeRequest = encodeResponse;
            var decodeResponse = strategy.Decode(decodeRequest);

            decodeResponse.Should().Be("Hello World!");
        }

        [Fact]
        public void TestUtf16EncodingStrategy()
        {
            var strategy = new Utf16EncodingStrategy();

            var encodeRequest = "Hello World!";
            var encodeResponse = strategy.Encode(encodeRequest);
            var expectedResponse = Encoding.Unicode.GetBytes(encodeRequest);

            encodeResponse.SequenceEqual(expectedResponse).Should().BeTrue();

            var decodeRequest = encodeResponse;
            var decodeResponse = strategy.Decode(decodeRequest);

            decodeResponse.Should().Be("Hello World!");
        }
    }
}
