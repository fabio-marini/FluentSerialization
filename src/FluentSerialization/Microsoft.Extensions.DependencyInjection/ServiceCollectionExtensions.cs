using FluentSerialization;
using FluentSerialization.Strategies;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentSerialization(this IServiceCollection services, Func<IServiceProvider, byte[]> encryptionKey = null)
        {
            if (encryptionKey != null)
            {
                services.AddTransient<IEncryptionStrategy>(sp => new AesEncryptionStrategy(encryptionKey(sp)));
            }

            return services

                .AddTransient<ISerializationStrategy, JsonSerializationStrategy>()
                .AddTransient<ISerializationStrategy, XmlSerializationStrategy>()

                // add all encoding strategies
                .AddTransient<IEncodingStrategy, Utf8EncodingStrategy>()
                .AddTransient<IEncodingStrategy, Utf16EncodingStrategy>()

                // add all compression strategies
                .AddTransient<ICompressionStrategy, GZipCompressionStrategy>()
                .AddTransient<ICompressionStrategy, BZip2CompressionStrategy>()
                .AddTransient<ICompressionStrategy, PassThruCompressionStrategy>()

                // add all encryption strategies
                .AddTransient<IEncryptionStrategy, PassThruEncryptionStrategy>();
        }
    }
}
