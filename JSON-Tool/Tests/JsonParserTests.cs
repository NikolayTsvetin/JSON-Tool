using System;
using JSON_Tool;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JsonSerializer.Tests
{
    [TestClass]
    public class JsonParserTests
    {
        [TestMethod]
        public void Should_Parse_Char()
        {
            // Arrange
            const char rawInput = 'x';
            string input = JsonConvert.SerializeObject(rawInput);
            object expected = JsonConvert.DeserializeObject(input);

            // Act
            object result = JsonParser<object>.Parse(input);

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(rawInput, char.Parse(result.ToString()));
        }

        [TestMethod]
        public void Should_Parse_String()
        {
            // Arrange
            const string rawInput = "asd\n\"test'";
            string input = JsonConvert.SerializeObject(rawInput);
            object expected = JsonConvert.DeserializeObject(input);

            // Act
            object result = JsonParser<object>.Parse(input);

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(rawInput, result.ToString());
        }

        [TestMethod]
        public void Should_Parse_Int()
        {
            // Arrange
            const int rawInput = 7;
            string input = JsonConvert.SerializeObject(rawInput);
            object expected = JsonConvert.DeserializeObject(input);

            // Act
            object result = JsonParser<object>.Parse(input);

            // Assert
            Assert.AreEqual(int.Parse(expected.ToString()), int.Parse(result.ToString()));
            Assert.AreEqual(rawInput, int.Parse(result.ToString()));
        }

        [TestMethod]
        public void Should_Parse_Double()
        {
            // Arrange
            const double rawInput = 7.7;
            string input = JsonConvert.SerializeObject(rawInput);
            object expected = JsonConvert.DeserializeObject(input);

            // Act
            object result = JsonParser<object>.Parse(input);

            // Assert
            Assert.AreEqual(double.Parse(expected.ToString()), double.Parse(result.ToString()));
            Assert.AreEqual(rawInput, double.Parse(result.ToString()));
        }

        [TestMethod]
        public void Should_Parse_Bool()
        {
            // Arrange
            const bool rawInput = true;
            string input = JsonConvert.SerializeObject(rawInput);
            object expected = JsonConvert.DeserializeObject(input);

            // Act
            object result = JsonParser<object>.Parse(input);

            // Assert
            Assert.AreEqual(bool.Parse(expected.ToString()), bool.Parse(result.ToString()));
            Assert.AreEqual(rawInput, bool.Parse(result.ToString()));
        }

        [TestMethod]
        public void Should_Parse_Object()
        {
            // Arrange
            object rawInput = new { x = 5, y = "asd" };
            string input = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(input);
            Dictionary<string, object> casted = (Dictionary<string, object>)result;
            
            // Assert
            Assert.AreEqual(5, casted["x"]);
            Assert.AreEqual("asd", casted["y"]);
        }

        [TestMethod]
        public void Should_Parse_Object_With_Nested_Object()
        {
            // Arrange
            object rawInput = new { x = 5, y = "asd", test = new { a = 2, c = 10, b = "dsds" } };
            string input = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(input);
            Dictionary<string, object> casted = (Dictionary<string, object>)result;
            Dictionary<string, object> inner = (Dictionary<string, object>)casted["test"];

            // Assert
            Assert.AreEqual(5, casted["x"]);
            Assert.AreEqual("asd", casted["y"]);
            Assert.AreEqual(2, inner["a"]);
            Assert.AreEqual(10, inner["c"]);
            Assert.AreEqual("dsds", inner["b"]);
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Objects()
        {
            // Arrange
            object rawInput = new { x = 5, y = "asd", test = new { a = 2, c = 10, b = "dsds" } };
            List<object> list = new List<object>() { rawInput, rawInput };
            string input = JsonConvert.SerializeObject(list);

            // Act
            object result = JsonParser<object>.Parse(input);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                ;
                Dictionary<string, object> wholeObject = (Dictionary<string, object>)casted[i];
                Dictionary<string, object> inner = (Dictionary<string, object>)wholeObject["test"];

                Assert.AreEqual(5, wholeObject["x"]);
                Assert.AreEqual("asd", wholeObject["y"]);
                Assert.AreEqual(2, inner["a"]);
                Assert.AreEqual(10, inner["c"]);
                Assert.AreEqual("dsds", inner["b"]);
            }
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Chars()
        {
            // Arrange
            List<char> rawInput = new List<char>() { 'x', '\n', '\'' };
            string serializedInput = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(serializedInput);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                Assert.AreEqual(char.Parse(casted[i].ToString()), rawInput[i]);
            }
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Strings()
        {
            // Arrange
            List<string> rawInput = new List<string>() { "asd", "\n", "test\ttest" };
            string serializedInput = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(serializedInput);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                Assert.AreEqual(casted[i], rawInput[i]);
            }
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Integers()
        {
            // Arrange
            List<int> rawInput = new List<int>() { 7, 5, 13, 23 };
            string serializedInput = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(serializedInput);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                Assert.AreEqual(casted[i], rawInput[i]);
            }
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Doubles()
        {
            // Arrange
            List<double> rawInput = new List<double>() { 7.3, 5.12, 13.0, 23.512 };
            string serializedInput = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(serializedInput);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                Assert.AreEqual(casted[i], rawInput[i]);
            }
        }

        [TestMethod]
        public void Should_Parse_IEnumerable_Of_Bools()
        {
            // Arrange
            bool[] rawInput = new bool[5] { true, true, true, false, true };
            string serializedInput = JsonConvert.SerializeObject(rawInput);

            // Act
            object result = JsonParser<object>.Parse(serializedInput);
            IList<object> casted = (IList<object>)result;

            // Assert
            for (int i = 0; i < casted.Count; i++)
            {
                Assert.AreEqual(bool.Parse(casted[i].ToString()), rawInput[i]);
            }
        }
    }
}
