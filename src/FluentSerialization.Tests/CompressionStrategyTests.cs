namespace FluentSerialization.Tests.Compression
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.Zip;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class CompressionStrategyTests
    {
        string longText = Enumerable.Range(1, 128).Select(i => $"Hello world {i}! :)").Aggregate((s1, s2) => $"{s1}\r\n{s2}");

        [TestMethod]
        public void TestPassThruCompressionStrategy()
        {
            var strategy = new PassThruCompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes("Hello World!");
            var compressResponse = strategy.Compress(compressRequest);

            compressResponse.SequenceEqual(compressRequest).Should().BeTrue();

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.SequenceEqual(decompressRequest).Should().BeTrue();
        }

        [TestMethod]
        public void TestGZipCompressionStrategy()
        {
            var strategy = new GZipCompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes(longText);
            var compressResponse = strategy.Compress(compressRequest);

            compressRequest.Length.Should().Be(2578);
            compressResponse.Length.Should().Be(325);

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.Length.Should().Be(2578);
        }

        [TestMethod]
        public void TestBZip2CompressionStrategy()
        {
            var strategy = new BZip2CompressionStrategy();

            var compressRequest = Encoding.UTF8.GetBytes(longText);
            var compressResponse = strategy.Compress(compressRequest);

            compressRequest.Length.Should().Be(2578);
            compressResponse.Length.Should().Be(223);

            var decompressRequest = compressResponse;
            var decompressResponse = strategy.Decompress(decompressRequest);

            decompressResponse.Length.Should().Be(2578);
        }

        [TestMethod, Ignore]
        public void TestZipArchivingStrategy()
        {
            var strategy = new ZipArchivingStrategy();

            var archiveRequest = Directory.GetFiles(@"G:\Temp\ziptests\logs").ToDictionary(f => f, f => File.ReadAllBytes(f));
            var archiveResponse = strategy.Archive(archiveRequest);

            File.WriteAllBytes(@"G:\Temp\ziptests\logs.zip", archiveResponse);

            //archiveRequest.Length.Should().Be(2578);
            //archiveResponse.Length.Should().Be(223);

            var extractRequest = archiveResponse;
            var extractResponse = strategy.Extract(extractRequest);

            foreach(var r in extractResponse)
            {
                var path = Path.Combine(@"G:\Temp\ziptests\extracted", r.Key);

                var directoryName = Path.GetDirectoryName(path);

                if (directoryName.Length > 0)
                {
                    Directory.CreateDirectory(directoryName);
                }

                File.WriteAllBytes(path, r.Value);
            }

            //extractResponse.Length.Should().Be(2578);
        }

        [TestMethod, Ignore]
        public void TestZipArchive()
        {
            // TODO: add support for password-protected archives
            var files = Directory.GetFiles(@"G:\Temp\ziptests\logs");
            var outputStream = new MemoryStream();

            using (var zipOutputStream = new ZipOutputStream(outputStream))
            {
                zipOutputStream.SetLevel(9);

                foreach (var file in files)
                {
                    var entry = new ZipEntry(file);
                    //{
                    //    DateTime = DateTime.Now,
                    //    Size = file.Filesize
                    //};

                    // create the entry in the output stream
                    zipOutputStream.PutNextEntry(entry);

                    // read the file stream to be archived
                    var fileStream = File.OpenRead(file);

                    // copy the file stream to the ZIP output stream (which will compress it)
                    fileStream.CopyTo(zipOutputStream);
                }

                zipOutputStream.Finish();

                zipOutputStream.Close();
            }

            File.WriteAllBytes(@"G:\Temp\ziptests\logs.zip", outputStream.ToArray());
        }

        [TestMethod, Ignore]
        public void TestZipExtract()
        {
            var archive = new ZipFile(@"G:\Temp\ziptests\logs.zip");

            var outFolder = @"G:\Temp\ziptests\extracted";

            var buffer = new byte[4096];

            foreach (ZipEntry zip in archive)
            {
                var fullZipToPath = Path.Combine(outFolder, zip.Name);

                var directoryName = Path.GetDirectoryName(fullZipToPath);

                if (directoryName.Length > 0)
                {
                    Directory.CreateDirectory(directoryName);
                }

                using (var zipStream = archive.GetInputStream(zip))

                using (Stream fsOutput = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipStream, fsOutput, buffer);
                }

            }

        }
    }
}
