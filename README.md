# FluentSerialization
A simple fluent API for the most common serialization tasks.

The API is defined by three interfaces: `IStringValue` for working with strings, `IBinaryValue` for byte arrays and `ITypeValue` for objects.

The API is accessed using the `With` class. This provides the methods to access the classes that implement the above interfaces.

The `Value` property provides access to the final value obtained after applying all the serialization tasks.

Please note that *serialization* is used both as a generic term (to indicate the entire process, e.g. encoding or compression) and as a specific term to indicate the serialization of an object to and from a string representation.

And since a picture paints a thousand words...

![Flow](/img/image.png)


## Strategies
Serialization tasks are implemented using one of the following interfaces:
- `IEncodingStrategy` provides methods to encode/decode strings into byte arrays using a specific encoding, e.g. UTF8
- `ISerializationStrategy` provides methods to serialize/deserialize objects into strings using a specific serializer, e.g. JSON
- `ICompressionStrategy` provides methods to compress/decompress byte arrays using a specific algorithm, e.g. GZip
- `IEncryptionStrategy` provides methods to encrypt/decrypt byte arrays, using a specific algorithm, e.g. AES

The `IStrategies` interface combines all of the above in a single convenient place. An implementation of this can then used with the fluent API. If an implementation is not provided, the API will default to UTF8 for encoding, JSON for serialization and pass thru for compression and encryption, i.e. don't compress or encrypt.

## Examples
```c#
            var secretAgent = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            // serialize the specified SecretAgent instance to a JSON string, i.e.
            // {"Name":"Bond. James Bond","Id":"007"}
            string jsonAgent = With.Object<SecretAgent>(secretAgent).Serialize().Value;

            // deserialize the specified JSON string into a new SecretAgent instance
            SecretAgent originalAgent = With.String(jsonAgent).Deserialize<SecretAgent>().Value;

            // serialize the specified SecretAgent instance to a byte array
            byte[] binaryAgent = With.Object<SecretAgent>(secretAgent).Serialize().Encode().Value;

            // this is the same as the above, because the default compression strategy is to pass thru
            byte[] notCompressedAgent = With.Object<SecretAgent>(secretAgent).Serialize().Encode().Compress().Value;

            // same as above + convert to base64 string, e.g. for storage or transmission, i.e.
            // eyJOYW1lIjoiQm9uZC4gSmFtZXMgQm9uZCIsIklkIjoiMDA3In0=
            string base64Customer = With.Object<SecretAgent>(secretAgent).Serialize().Encode().ToBase64().Value;

            // TestStrategies implements IStrategies and specifies a set of strategies to use
            var opt = new TestStrategies
            {
                Encoding = new Utf16EncodingStrategy(),
                Encryption = new AesEncryptionStrategy(privateKey),
                Compression = new GZipCompressionStrategy(),
                Serialization = new XmlSerializationStrategy()
            };

            // serialize the specified SecretAgent to an XML string (note the opt parameter passed in to the Object method), i.e.
            // <Root><Name>Bond. James Bond</Name><Id>007</Id></Root>
            string xmlAgent = With.Object<SecretAgent>(secretAgent, opt).Serialize().Value;

            // serialize the specified SecretAgent to a byte array and encrypt
            byte[] encryptedAgent = With.Object<SecretAgent>(secretAgent, opt).Serialize().Encode().Encrypt().Value;

            // decrypt and deserialize the encrypted byte array obtained above
            SecretAgent decryptedAgent = With.Bytes(encryptedAgent, opt).Decrypt().Decode().Deserialize<SecretAgent>().Value;
```
