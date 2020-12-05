namespace FluentSerialization
{
    /// <summary>
    /// Represents an encryption strategy
    /// </summary>
    public interface IEncryptionStrategy
    {
        /// <summary>
        /// Decrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to decrypt (obtained by calling <see cref="Encrypt(byte[])" />)</param>
        /// <returns>The decrypted byte array</returns>
        byte[] Decrypt(byte[] req);

        /// <summary>
        /// Encrypt the specified byte array
        /// </summary>
        /// <param name="req">The byte array to encrypt</param>
        /// <returns>The encrypted byte array</returns>
        byte[] Encrypt(byte[] req);
    }
}