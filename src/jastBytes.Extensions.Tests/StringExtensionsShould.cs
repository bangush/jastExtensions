using System;
using Xunit;

namespace jastBytes.Extensions.Tests
{
    public class StringExtensionsShould
    {
        [Fact]
        public void EncryptAndDecrypt()
        {
            const string text = "Hello World!";
            const string secretKey = "secret-string-to-encrypt";

            var encryptedString = text.Encrypt(secretKey);
            Assert.NotNull(encryptedString);

            var decryptedString = encryptedString.Decrypt(secretKey);
            Assert.Equal(text, decryptedString);
        }

        [Theory]
        [InlineData("hello world", "helloWorld")]
        [InlineData("secret-string-to-camel", "secretStringToCamel")]
        public void CamelCase(string testString, string expected)
        {
            var camelCase = testString.ToCamelCase();
            Assert.NotNull(camelCase);
            Assert.Equal(expected, camelCase);
        }
    }
}
