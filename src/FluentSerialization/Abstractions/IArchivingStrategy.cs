using System.Collections.Generic;

namespace FluentSerialization
{
    /// <summary>
    /// Represents an archiving strategy
    /// </summary>
    public interface IArchivingStrategy
    {
        /// <summary>
        /// Archive the specified <see cref="IDictionary{string, byte[]}" />
        /// </summary>
        /// <param name="req">The <see cref="IDictionary{string, byte[]}" /> to archive</param>
        /// <returns>An archive that contains the <see cref="IEnumeIDictionaryrable{string, byte[]}" /></returns>
        byte[] Archive(IDictionary<string, byte[]> req);

        /// <summary>
        /// Extract the contents of the specified archive
        /// </summary>
        /// <param name="req">A byte array that represents the archive to extract</param>
        /// <returns>The contents of the specified archive (obtained by calling <see cref="Archive(IDictionary{string, byte[]})" />)</returns>
        IDictionary<string, byte[]> Extract(byte[] req);
    }
}
