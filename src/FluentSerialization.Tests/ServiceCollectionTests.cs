namespace FluentSerialization.Tests
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ServiceCollectionTests
    {
        public static IEnumerable<object[]> PrivateKeys()
        {
            yield return new object[] { default(byte[]) };
            yield return new object[] { new byte[0] };
            yield return new object[] { AesEncryptionStrategy.GeneratePrivateKey() };
            yield return new object[] { Convert.FromBase64String("vibaOtNImyzWYyqdkWm3LsX53dg9fp+gMrhd8vbxAw8=") };
            yield return new object[] { Convert.FromBase64String("ElOwc3Xtni8vxtDyYO2vMPPcgjT71NSjKDcVVseBCqM=") };
        }

        [MemberData(nameof(PrivateKeys))]
        [Theory(DisplayName = "Strategies are loaded")]
        public void Test(byte[] encryptionKey)
        {
            var svc = new ServiceCollection().AddFluentSerialization(encryptionKey).BuildServiceProvider();

            svc.GetServices<ISerializationStrategy>().Should().HaveCount(2);
            svc.GetServices<ISerializationStrategy>().OfType<JsonSerializationStrategy>().Single().Should().NotBeNull();
            svc.GetServices<ISerializationStrategy>().OfType<XmlSerializationStrategy>().Single().Should().NotBeNull();

            svc.GetServices<IEncodingStrategy>().Should().HaveCount(2);
            svc.GetServices<IEncodingStrategy>().OfType<Utf8EncodingStrategy>().Single().Should().NotBeNull();
            svc.GetServices<IEncodingStrategy>().OfType<Utf16EncodingStrategy>().Single().Should().NotBeNull();

            svc.GetServices<ICompressionStrategy>().Should().HaveCount(3);
            svc.GetServices<ICompressionStrategy>().OfType<GZipCompressionStrategy>().Single().Should().NotBeNull();
            svc.GetServices<ICompressionStrategy>().OfType<BZip2CompressionStrategy>().Single().Should().NotBeNull();
            svc.GetServices<ICompressionStrategy>().OfType<PassThruCompressionStrategy>().Single().Should().NotBeNull();

            if (encryptionKey != null && encryptionKey.Length > 0)
            {
                svc.GetServices<IEncryptionStrategy>().Should().HaveCount(2);
                svc.GetServices<IEncryptionStrategy>().OfType<PassThruEncryptionStrategy>().Single().Should().NotBeNull();
                svc.GetServices<IEncryptionStrategy>().OfType<AesEncryptionStrategy>().FirstOrDefault().Should().NotBeNull();
            }
            else
            {
                svc.GetServices<IEncryptionStrategy>().Should().HaveCount(1);
                svc.GetServices<IEncryptionStrategy>().OfType<PassThruEncryptionStrategy>().Single().Should().NotBeNull();
                svc.GetServices<IEncryptionStrategy>().OfType<AesEncryptionStrategy>().FirstOrDefault().Should().BeNull();
            }
        }
    }
}
