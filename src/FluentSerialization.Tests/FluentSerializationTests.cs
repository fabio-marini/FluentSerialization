namespace FluentSerialization.Tests
{
    using FluentAssertions;
    using FluentSerialization;
    using Moq;
    using System;
    using System.Linq;
    using Xunit;

    public class FluentSerializationTests
    {
        class SecretAgent
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        #region Base64

        [Fact]
        public void WhenConvertBase64StringToBytes()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var req1 = Convert.ToBase64String(bytes);
            var val1 = new StringValue(req1, null);
            var res1 = val1.FromBase64().Value;

            res1.SequenceEqual(bytes).Should().BeTrue();
        }

        [Fact]
        public void WhenConvertBytesToBase64String()
        {
            var req1 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val1 = new BinaryValue(req1, null);
            var res1 = val1.ToBase64().Value;

            res1.Should().Be(Convert.ToBase64String(req1));
        }

        #endregion

        #region Encoding

        [Fact]
        public void WhenEncodeAndSuppliedStrategy()
        {
            var mockRequest = "Hello world! :)";
            var mockResponse = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");

            var mockStrategy = new Mock<IEncodingStrategy>();
            mockStrategy.Setup(m => m.Encode(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Encoding = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new StringValue(req1, opt);
            var res1 = val1.Encode().Value;

            res1.SequenceEqual(mockResponse).Should().BeTrue();
            mockStrategy.Verify(m => m.Encode(req1), Times.Once);
        }

        [Fact]
        public void WhenEncodeAndDefaultStrategy()
        {
            var req1 = "Hello world! :)";
            var val1 = new StringValue(req1, null);
            var res1 = val1.Encode().Value;

            res1.SequenceEqual(System.Text.Encoding.UTF8.GetBytes("Hello world! :)")).Should().BeTrue();
        }

        [Fact]
        public void WhenDecodeAndSuppliedStrategy()
        {
            var mockRequest = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var mockResponse = "Hello world! :)";

            var mockStrategy = new Mock<IEncodingStrategy>();
            mockStrategy.Setup(m => m.Decode(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Encoding = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new BinaryValue(req1, opt);
            var res1 = val1.Decode().Value;

            res1.Should().Be(mockResponse);
            mockStrategy.Verify(m => m.Decode(req1), Times.Once);
        }

        [Fact]
        public void WhenDecodeAndDefaultStrategy()
        {
            var req1 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val1 = new BinaryValue(req1, null);
            var res1 = val1.Decode().Value;

            res1.Should().Be("Hello world! :)");
        }

        #endregion

        #region Compression

        [Fact]
        public void WhenCompressAndSuppliedStrategy()
        {
            var mockRequest = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var mockResponse = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var mockStrategy = new Mock<ICompressionStrategy>();
            mockStrategy.Setup(m => m.Compress(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Compression = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new BinaryValue(req1, opt);
            var res1 = val1.Compress().Value;

            res1.SequenceEqual(mockResponse).Should().BeTrue();
            mockStrategy.Verify(m => m.Compress(req1), Times.Once);
        }

        [Fact]
        public void WhenDecompressAndSuppliedStrategy()
        {
            var mockRequest = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var mockResponse = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var mockStrategy = new Mock<ICompressionStrategy>();
            mockStrategy.Setup(m => m.Decompress(mockResponse)).Returns(mockRequest);

            var opt = new SerializationOptions
            {
                Compression = mockStrategy.Object
            };

            var req2 = mockResponse;
            var val2 = new BinaryValue(req2, opt);
            var res2 = val2.Decompress().Value;

            res2.SequenceEqual(mockRequest).Should().BeTrue();
            mockStrategy.Verify(m => m.Decompress(req2), Times.Once);
        }

        [Fact]
        public void WhenCompressAndDefaultStrategy()
        {
            var req1 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val1 = new BinaryValue(req1, null);
            var res1 = val1.Compress().Value;

            res1.SequenceEqual(req1).Should().BeTrue();
        }

        [Fact]
        public void WhenDecompressAndDefaultStrategy()
        {
            var req2 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val2 = new BinaryValue(req2, null);
            var res2 = val2.Decompress().Value;

            res2.SequenceEqual(req2).Should().BeTrue();
        }

        #endregion

        #region Encryption

        [Fact]
        public void WhenEncryptAndSuppliedStrategy()
        {
            var mockRequest = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var mockResponse = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var mockStrategy = new Mock<IEncryptionStrategy>();
            mockStrategy.Setup(m => m.Encrypt(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Encryption = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new BinaryValue(req1, opt);
            var res1 = val1.Encrypt().Value;

            res1.SequenceEqual(mockResponse).Should().BeTrue();
            mockStrategy.Verify(m => m.Encrypt(req1), Times.Once);
        }

        [Fact]
        public void WhenDecryptAndSuppliedStrategy()
        {
            var mockRequest = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var mockResponse = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            var mockStrategy = new Mock<IEncryptionStrategy>();
            mockStrategy.Setup(m => m.Decrypt(mockResponse)).Returns(mockRequest);

            var opt = new SerializationOptions
            {
                Encryption = mockStrategy.Object
            };

            var req2 = mockResponse;
            var val2 = new BinaryValue(req2, opt);
            var res2 = val2.Decrypt().Value;

            res2.SequenceEqual(mockRequest).Should().BeTrue();
            mockStrategy.Verify(m => m.Decrypt(req2), Times.Once);
        }

        [Fact]
        public void WhenEncryptAndDefaultStrategy()
        {
            var req1 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val1 = new BinaryValue(req1, null);
            var res1 = val1.Encrypt().Value;

            res1.SequenceEqual(req1).Should().BeTrue();
        }

        [Fact]
        public void WhenDecryptAndDefaultStrategy()
        {
            var req2 = System.Text.Encoding.UTF8.GetBytes("Hello world! :)");
            var val2 = new BinaryValue(req2, null);
            var res2 = val2.Decrypt().Value;

            res2.SequenceEqual(req2).Should().BeTrue();
        }

        #endregion

        #region Serialization

        [Fact]
        public void WhenSerializeAndSuppliedStrategy()
        {
            var mockRequest = new SecretAgent { Id = "007", Name = "Bond. James Bond" };
            var mockResponse = "{\"Name\":\"Bond. James Bond\",\"Id\":\"007\"}";

            var mockStrategy = new Mock<ISerializationStrategy>();
            mockStrategy.Setup(m => m.Serialize<SecretAgent>(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Serialization = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new TypeValue<SecretAgent>(req1, opt);
            var res1 = val1.Serialize().Value;

            res1.Should().NotBeNull();
            res1.Should().Be(mockResponse);
            mockStrategy.Verify(m => m.Serialize(req1), Times.Once);
        }

        [Fact]
        public void WhenSerializeAndDefaultStrategy()
        {
            var req1 = new SecretAgent { Id = "007", Name = "Bond. James Bond" };
            var val1 = new TypeValue<SecretAgent>(req1, null);
            var res1 = val1.Serialize().Value;

            res1.Should().NotBeNull();
            res1.Should().Be("{\"Name\":\"Bond. James Bond\",\"Id\":\"007\"}");
        }

        [Fact]
        public void WhenDeserializeAndSuppliedStrategy()
        {
            var mockRequest = "{\"Name\":\"Bond. James Bond\",\"Id\":\"007\"}";
            var mockResponse = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            var mockStrategy = new Mock<ISerializationStrategy>();
            mockStrategy.Setup(m => m.Deserialize<SecretAgent>(mockRequest)).Returns(mockResponse);

            var opt = new SerializationOptions
            {
                Serialization = mockStrategy.Object
            };

            var req1 = mockRequest;
            var val1 = new StringValue(req1, opt);
            var res1 = val1.Deserialize<SecretAgent>().Value;

            res1.Should().NotBeNull();
            res1.Name.Should().Be("Bond. James Bond");
            res1.Id.Should().Be("007");
            mockStrategy.Verify(m => m.Deserialize<SecretAgent>(req1), Times.Once);
        }

        [Fact]
        public void WhenDeserializeAndDefaultStrategy()
        {
            var req1 = "{\"Name\":\"Bond. James Bond\",\"Id\":\"007\"}";
            var val1 = new StringValue(req1, null);
            var res1 = val1.Deserialize<SecretAgent>().Value;

            res1.Should().NotBeNull();
            res1.Name.Should().Be("Bond. James Bond");
            res1.Id.Should().Be("007");
        }

        #endregion

    }
}
