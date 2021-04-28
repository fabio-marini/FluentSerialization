namespace FluentSerialization.Tests
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class EncryptionStrategyTests
    {
        [Fact]
        public void TestPassThruEncryptionStrategy()
        {
            var strategy = new PassThruEncryptionStrategy();

            var encryptRequest = Encoding.UTF8.GetBytes("Hello World!");
            var encryptResponse = strategy.Encrypt(encryptRequest);

            encryptResponse.SequenceEqual(encryptRequest).Should().BeTrue();

            var decryptRequest = encryptResponse;
            var decryptResponse = strategy.Decrypt(decryptRequest);

            decryptResponse.SequenceEqual(decryptRequest).Should().BeTrue();
        }

        public static IEnumerable<object[]> PrivateKeys()
        {
            yield return new object[] { Convert.FromBase64String("vibaOtNImyzWYyqdkWm3LsX53dg9fp+gMrhd8vbxAw8=") };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
        }

        [Theory]
        [MemberData(nameof(PrivateKeys))]
        public void TestAesEncryptionStrategy(byte[] privateKey)
        {
            var strategy = new AesEncryptionStrategy(privateKey);

            var encryptRequest = Encoding.UTF8.GetBytes("Hello World!");
            var encryptResponse = strategy.Encrypt(encryptRequest);

            encryptResponse.Length.Should().Be(32);

            // test using a new instance of the strategy
            strategy = new AesEncryptionStrategy(privateKey);

            var decryptRequest = encryptResponse;
            var decryptResponse = strategy.Decrypt(decryptRequest);

            decryptResponse.SequenceEqual(encryptRequest).Should().BeTrue();
        }
    }
}
