using System;
using JSON_Tool;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JsonSerializer.Tests
{
    [TestClass]
    public class JsonSerializerTests
    {
        [TestMethod]
        public void Should_Serialize_String()
        {
            // Arrange
            string input = "asd\n\t\'\"some other text here";
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Char()
        {
            // Arrange
            char input = 'x';
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Integer()
        {
            // Arrange
            int input = 7;
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Bool()
        {
            // Arrange
            bool input = true;
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Decimal()
        {
            // Arrange
            decimal input = 23.123456789M;
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Double()
        {
            // Arrange
            double input = 23.7;
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Object()
        {
            // Arrange
            object input = new { x = 5, y = "asd", z = new int[2] { 1, 2 } };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Strings_Collection()
        {
            // Arrange
            List<string> input = new List<string>() {
                "asd\n\t\'\"some other text here",
                "hello",
                "world!"
            };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Objects_Collection()
        {
            // Arrange
            object obj = new { x = 5, y = "asd", z = new int[2] { 1, 2 } };
            List<object> input = new List<object>()
            {
                obj,
                obj
            };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Collection_Of_Collections_S()
        {
            // Arrange
            List<List<string>> input = new List<List<string>>()
            {
                new List<string>() { "asd", "dsa", "sda" },
                new List<string>() { "hey\"\t" }
            };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Collection_Of_Collections_N()
        {
            // Arrange
            List<List<int>> input = new List<List<int>>()
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 4, 100, 200, 500, 1000 }
            };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void Should_Serialize_Collection_Of_Collections_O()
        {
            // Arrange
            List<List<object>> input = new List<List<object>>()
            {
                new List<object>() { new { x = "asd", y = "dsa"} },
                new List<object>() { new { a = 5, b = new int[3] { 1, 2, 3 } } }
            };
            string expectedOutput = JsonConvert.SerializeObject(input);

            // Act
            string result = JSON_Tool.JsonSerializer.Serialize(input);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }
    }
}
