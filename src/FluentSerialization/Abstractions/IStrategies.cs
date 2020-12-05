namespace FluentSerialization
{

    /// <summary>
    /// The strategies to use, all in one convenient place
    /// </summary>
    public interface IStrategies
    {
        /// <summary>
        /// The <see cref="IEncodingStrategy"/> to use
        /// </summary>
        public IEncodingStrategy Encoding { get; set; }

        /// <summary>
        /// The <see cref="ICompressionStrategy"/> to use
        /// </summary>
        public ICompressionStrategy Compression { get; set; }

        /// <summary>
        /// The <see cref="IEncryptionStrategy"/> to use
        /// </summary>
        public IEncryptionStrategy Encryption { get; set; }

        /// <summary>
        /// The <see cref="ISerializationStrategy"/> to use
        /// </summary>
        public ISerializationStrategy Serialization { get; set; }

        /// <summary>
        /// The <see cref="IArchivingStrategy"/> to use
        /// </summary>
        public IArchivingStrategy Archiving { get; set; }
    }
}
