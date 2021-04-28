using FluentSerialization;
using FluentSerialization.Strategies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentSerialization(this IServiceCollection services, byte[] encryptionKey = null)
        {
            if (encryptionKey != null && encryptionKey.Length > 0)
            {
                services.AddTransient<IEncryptionStrategy>(sp => new AesEncryptionStrategy(encryptionKey));
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
