namespace FluentSerialization.Strategies
{
    /// <summary>
    /// Pass thru encryption strategy
    /// </summary>
    public class PassThruEncryptionStrategy : IEncryptionStrategy
    {
        /// <summary>
        /// Encrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to encrypt</param>
        /// <returns>The encrypted byte array</returns>
        public byte[] Encrypt(byte[] req) => req;

        /// <summary>
        /// Decrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decrypt (obtained by calling <see cref="Encrypt(byte[])" />)</param>
        /// <returns>The decrypted byte array</returns>
        public byte[] Decrypt(byte[] req) => req;
    }
}
