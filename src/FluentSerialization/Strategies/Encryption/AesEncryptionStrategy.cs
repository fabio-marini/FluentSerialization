namespace FluentSerialization.Strategies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    /// <summary>
    /// AES (symmetric) encryption strategy
    /// </summary>
    public class AesEncryptionStrategy : IEncryptionStrategy
    {
        /// <summary>
        /// Generates a private key as a byte[32] for use with the <see cref="AesEncryptionStrategy"/>
        /// </summary>
        /// <returns>The private key as a byte[32] for use with the <see cref="AesEncryptionStrategy"/></returns>
        public static byte[] GeneratePrivateKey()
        {
            var privateKey = new byte[32];

            new Random().NextBytes(privateKey);

            return privateKey;
        }

        private readonly byte[] privateKey;

        /// <summary>
        /// Create an instance of the <see cref="AesEncryptionStrategy"/>
        /// </summary>
        /// <param name="privateKey">The private key used for encryption</param>
        public AesEncryptionStrategy(byte[] privateKey)
        {
            this.privateKey = privateKey;
        }

        /// <summary>
        /// Encrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to encrypt</param>
        /// <returns>The encrypted byte array</returns>
        public byte[] Encrypt(byte[] req)
        {
            IEnumerable<byte> result;

            using (var aes = Aes.Create())
            {
                // use a new IV for each encryption
                aes.GenerateIV();
                aes.Key = privateKey;

                using (var cryptoTransform = aes.CreateEncryptor())
                {
                    result = cryptoTransform.TransformFinalBlock(req, 0, req.Length);

                    // get IV before Dispose() and append to result, as it will be required for decryption...
                    return result.Concat(aes.IV).ToArray(); ;
                }
            }
        }

        /// <summary>
        /// Decrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decrypt (obtained by calling <see cref="Encrypt(byte[])" />)</param>
        /// <returns>The decrypted byte array</returns>
        public byte[] Decrypt(byte[] req)
        {
            using (var aes = Aes.Create())
            {
                // extract encrypted data and IV (the last 16 bytes) from request 
                var data = req.Take(req.Length - 16).ToArray();
                var iv = req.Skip(req.Length - 16).Take(16).ToArray();

                aes.IV = iv;
                aes.Key = privateKey;

                using (var cryptoTransform = aes.CreateDecryptor())
                {
                    return cryptoTransform.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }
    }
}
