﻿namespace FluentSerialization.Tests.Serialization
{
    using FluentAssertions;
    using FluentSerialization.Strategies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [TestClass]
    public class SerializationStrategyTests
    {
        class SecretAgent
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        [TestMethod]
        public void TestJsonSerializationStrategyWithDefaultSettings()
        {
            var strategy = new JsonSerializationStrategy();

            var serializeRequest = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            var serializeResponse = strategy.Serialize(serializeRequest);
            var expectedResponse = "{\"Name\":\"Bond. James Bond\",\"Id\":\"007\"}";

            serializeResponse.Equals(expectedResponse).Should().BeTrue();

            var deserializeRequest = serializeResponse;
            var deserializeResponse = strategy.Deserialize<SecretAgent>(deserializeRequest);

            deserializeResponse.Should().NotBeNull();
            deserializeResponse.Name.Should().Be("Bond. James Bond");
            deserializeResponse.Id.Should().Be("007");
        }

        [TestMethod]
        public void TestJsonSerializationStrategyWithCustomSettings()
        {
            var customSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var strategy = new JsonSerializationStrategy(customSettings);

            var serializeRequest = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            var serializeResponse = strategy.Serialize(serializeRequest);
            var expectedResponse = "{\"name\":\"Bond. James Bond\",\"id\":\"007\"}";

            serializeResponse.Equals(expectedResponse).Should().BeTrue();

            var deserializeRequest = serializeResponse;
            var deserializeResponse = strategy.Deserialize<SecretAgent>(deserializeRequest);

            deserializeResponse.Should().NotBeNull();
            deserializeResponse.Name.Should().Be("Bond. James Bond");
            deserializeResponse.Id.Should().Be("007");
        }

        [TestMethod]
        public void TestXmlSerializationStrategyWithCustomSettings()
        {
            var customSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            var strategy = new XmlSerializationStrategy(customSettings);

            var serializeRequest = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            var serializeResponse = strategy.Serialize(serializeRequest);
            var expectedResponse = "<Root><name>Bond. James Bond</name><id>007</id></Root>";

            serializeResponse.Equals(expectedResponse).Should().BeTrue();

            var deserializeRequest = serializeResponse;
            var deserializeResponse = strategy.Deserialize<SecretAgent>(deserializeRequest);

            deserializeResponse.Should().NotBeNull();
            deserializeResponse.Name.Should().Be("Bond. James Bond");
            deserializeResponse.Id.Should().Be("007");
        }

        [TestMethod]
        public void TestXmlSerializationStrategyWithDefaultSettings()
        {
            var strategy = new XmlSerializationStrategy();

            var serializeRequest = new SecretAgent { Id = "007", Name = "Bond. James Bond" };

            var serializeResponse = strategy.Serialize(serializeRequest);
            var expectedResponse = "<Root><Name>Bond. James Bond</Name><Id>007</Id></Root>";

            serializeResponse.Equals(expectedResponse).Should().BeTrue();

            var deserializeRequest = serializeResponse;
            var deserializeResponse = strategy.Deserialize<SecretAgent>(deserializeRequest);

            deserializeResponse.Should().NotBeNull();
            deserializeResponse.Name.Should().Be("Bond. James Bond");
            deserializeResponse.Id.Should().Be("007");
        }
    }
}