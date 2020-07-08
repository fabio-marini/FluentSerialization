namespace FluentSerialization.Tests.Encryption
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class EncryptionStrategyTests
    {
        [TestMethod]
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

        [TestMethod]
        public void TestAesEncryptionStrategy()
        {
            // get AES private key from config or environment variable
            // the private key is a byte[32] and this values is a base64 string representation of that
            var configRoot = new ConfigurationBuilder()
                .AddUserSecrets("15d5a906-fcb8-4743-a116-c014ec361282")
                .AddEnvironmentVariables()
                .Build();

            var aes = configRoot.GetSection("Encryption").GetSection("Aes");

            var base64Key = aes.GetChildren().FirstOrDefault(k => k.Key == "PrivateKey")?.Value;

            if(string.IsNullOrEmpty(base64Key))
            {
                // try environment variables...
                base64Key = Environment.GetEnvironmentVariable("ENCRYPTION__AES__PRIVATEKEY", EnvironmentVariableTarget.Process);
            }

            var privateKey = Convert.FromBase64String(base64Key);

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
