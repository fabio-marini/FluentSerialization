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

        public static IEnumerable<object[]> ValidPrivateKeys()
        {
            yield return new object[] { Convert.FromBase64String("vibaOtNImyzWYyqdkWm3LsX53dg9fp+gMrhd8vbxAw8=") };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
        }

        public static IEnumerable<object[]> InvalidPrivateKeys()
        {
            yield return new object[] { default(byte[]) };
            yield return new object[] { new byte[0] };
        }

        [Theory]
        [MemberData(nameof(InvalidPrivateKeys))]
        public void TestAesEncryptionStrategy1(byte[] privateKey)
        {
            try
            {
                var strategy = new AesEncryptionStrategy(privateKey);

                throw new Exception("Test did not throw expected exception!");
            }
            catch (ArgumentException ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Theory]
        [MemberData(nameof(ValidPrivateKeys))]
        public void TestAesEncryptionStrategy2(byte[] privateKey)
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
