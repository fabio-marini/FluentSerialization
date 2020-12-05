namespace FluentSerialization.Strategies
{
    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.Zip;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ZipArchivingStrategy : IArchivingStrategy
    {
        private byte[] buffer = new byte[4096];

        public byte[] Archive(IDictionary<string, byte[]> req)
        {
            var outputStream = new MemoryStream();

            using (var zipOutputStream = new ZipOutputStream(outputStream))
            {
                zipOutputStream.SetLevel(9);

                foreach (var key in req.Keys)
                {
                    var entry = new ZipEntry(key)
                    {
                        DateTime = DateTime.Now,
                        Size = req[key].Length
                    };

                    // create the entry in the output stream
                    zipOutputStream.PutNextEntry(entry);

                    // the stream to be archived
                    var entryStream = new MemoryStream(req[key]);

                    // copy the stream to be archived to the ZIP output stream (which will compress it)
                    StreamUtils.Copy(entryStream, zipOutputStream, buffer);
                }

                zipOutputStream.Finish();

                zipOutputStream.Close();
            }

            return outputStream.ToArray();
        }

        public IDictionary<string, byte[]> Extract(byte[] req)
        {
            var result = new Dictionary<string, byte[]>();

            using (var zipInputStream = new ZipInputStream(new MemoryStream(req)))
            {
                ZipEntry zip;

                while ((zip = zipInputStream.GetNextEntry()) != null)
                {
                    using (var zipStream = new MemoryStream())
                    {
                        StreamUtils.Copy(zipInputStream, zipStream, buffer);

                        result.Add(zip.Name, zipStream.ToArray());
                    }
                }

                zipInputStream.Close();
            }

            return result;

            //var archive = new ZipFile(new MemoryStream(req));


            //foreach (ZipEntry zip in archive)
            //{
            //    using var zipStream = archive.GetInputStream(zip);

            //    using (var fsOutput = new MemoryStream())
            //    {
            //        StreamUtils.Copy(zipStream, fsOutput, buffer);

            //        result.Add(zip.Name, fsOutput.ToArray());
            //    }
            //}

            //return result;
        }
    }
}
