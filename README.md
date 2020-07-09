# FluentSerialization
A simple fluent API for the most common serialization tasks.

Access the API using the `With` class. This provides the methods required to work with objects, strings and byte arrays.

Once finished, use the `Value` property to access the result obtained from executing all the serialization tasks.

And since a picture paints a thousand words...

![Flow](/img/image.png)

## Strategies
Each serialization task is implemented using one of the following interfaces:
- `IEncodingStrategy`, which provides methods to encode/decode strings into byte arrays using a specific encoding, e.g. UTF8
- `ISerializationStrategy`, which provides methods to serialize/deserialize objects into strings using a specific serializer, e.g. JSON
- `ICompressionStrategy`, which provides methods to compress/decompress byte arrays using a specific algorithm, e.g. GZip
- `IEncryptionStrategy`, which provides methods to encrypt/decrypt byte arrays, using a specific algorithm, e.g. AES

The `IStrategies` interface combines all of the above in a single convenient place. An implementation of this can then be provided to the fluent API. If an implementation is not provided, the API will default to UTF8 for encoding, JSON for serialization and pass thru for compression and encryption, i.e. don't compress or encrypt.

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

// TestStrategies implements IStrategies and is used to specify a set of strategies to use
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
## Download
You can download the NuGet package from [NuGet.org](https://www.nuget.org/packages/FluentSerialization/)