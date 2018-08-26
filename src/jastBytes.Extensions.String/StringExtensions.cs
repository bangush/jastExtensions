// MIT License
// 
// Copyright (c) 2018 Jan Steffen
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// Extensions for the string class
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Dictionary<char, string> UmlautMapping = new Dictionary<char, string>
        {
            { 'ä', "ae" },
            { 'ö', "oe" },
            { 'ü', "ue" },
            { 'Ä', "Ae" },
            { 'Ö', "Oe" },
            { 'Ü', "Ue" },
            { 'ß', "ss" }
        };

        /// <summary>
        /// Check whether this string is a valid url
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string text)
        {
            var rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return rx.IsMatch(text);
        }

        /// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
        /// <param name="input">The string to search for a match. </param>
        /// <param name="pattern">The regular expression pattern to match. </param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>Indicates whether the specified regular expression finds a match in the specified input string.</summary>
        /// <param name="input">The string to search for a match. </param>
        /// <param name="pattern">The regular expression pattern to match. </param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// Method removes all tag like stuff from string
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string StripHtml(this string html)
        {
            return string.IsNullOrEmpty(html) ? string.Empty : Regex.Replace(html, @"<[^>]*>", string.Empty);
        }

        /// <summary>
        /// Returns null if the string is null or whitespace. Otherwise returns the trimmed string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string NullIfNullOrWhiteSpace(this string input)
        {
            var trimmed = input?.Trim();
            return string.IsNullOrWhiteSpace(trimmed) ? null : trimmed;
        }

        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="text">string that will be truncated</param>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <param name="suffix">the suffix to append</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength, string suffix = "...")
        {
            var truncatedString = text;

            if (maxLength <= 0)
                return truncatedString;
            var strLength = maxLength - suffix.Length;

            if (strLength <= 0)
                return truncatedString;

            if (text == null || text.Length <= maxLength)
                return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        /// <summary>
        /// Breaks the given text into lines with a max length per row and breaks at blanks
        /// </summary>
        /// <param name="text">string that will be broken</param>
        /// <param name="maxLength">total length of characters to maintain before the break line happens</param>
        /// <param name="newLineSymbol">the symbol to split lines with</param>
        /// <returns>truncated string</returns>
        public static string BreakLines(this string text, int maxLength, string newLineSymbol = "\n")
        {
            text = text?.Trim();

            maxLength = maxLength - newLineSymbol.Length;
            if (maxLength <= 0)
                return text;

            if (text == null || text.Length <= maxLength)
                return text;

            var currentIndex = 0;
            var truncatedString = "";
            while (currentIndex < text.Length)
            {
                var substring = text.Substring(currentIndex, Math.Min(maxLength, text.Length - currentIndex));
                if (currentIndex + substring.Length >= text.Length)
                    return truncatedString + substring;

                var lastIndex = substring.LastIndexOf(' ');
                if (lastIndex == -1)
                    return text;
                truncatedString += text.Substring(currentIndex, lastIndex) + newLineSymbol;
                currentIndex = currentIndex + lastIndex + 1;
            }

            return truncatedString;
        }

        /// <summary>
        ///  Replaces the format item in a specified System.String with the text equivalent
        ///  of the value of a specified System.Object instance.
        /// </summary>
        /// <param name="value">A composite format string</param>
        /// <param name="args">An System.Object array containing zero or more objects to format.</param>
        /// <returns>A copy of format in which the format items have been replaced by the System.String
        /// equivalent of the corresponding instances of System.Object in args.</returns>
        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// Converts string to enum object
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Check whether the string is a valid phone number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string input)
        {
            var match = Regex.Match(input, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Check whether the string is a valid mail address
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmail(this string input)
        {
            var match = Regex.Match(input, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            return match.Success;
        }

        /// <summary>
        /// Convert to double
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static double ToDouble(this string input, bool throwExceptionIfFailed = false)
        {
            var valid = double.TryParse(input, NumberStyles.AllowDecimalPoint,
                new NumberFormatInfo { NumberDecimalSeparator = "." }, out var result);
            if (valid)
                return result;
            if (throwExceptionIfFailed)
                throw new FormatException($"'{input}' cannot be converted as double");
            return result;
        }

        /// <summary>
        /// Convert to int
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static int ToInt(this string input, int defaultValue = 0, bool throwExceptionIfFailed = true)
        {
            var valid = int.TryParse(input, out var result);
            if (valid)
                return result;
            if (throwExceptionIfFailed)
            {
                throw new FormatException($"'{input}' cannot be converted as int");
            }
            return defaultValue;
        }

        /// <summary>
        /// Convert to int
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static int? ToNullableInt(this string input, bool throwExceptionIfFailed = false)
        {
            var valid = int.TryParse(input, out var result);
            if (valid)
                return result;
            if (throwExceptionIfFailed)
            {
                throw new FormatException($"'{input}' cannot be converted as int");
            }
            return null;
        }

        // <summary>
        /// Convert to bool
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultValue"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static bool ToBool(this string input, bool defaultValue = false, bool throwExceptionIfFailed = true)
        {
            var valid = bool.TryParse(input, out var result);
            if (valid)
                return result;
            if (throwExceptionIfFailed)
            {
                throw new FormatException($"'{input}' cannot be converted as int");
            }
            return defaultValue;
        }

        /// <summary>
        /// Convert to nullable bool
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwExceptionIfFailed"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static bool? ToNullableBool(this string input, bool throwExceptionIfFailed = false)
        {
            var valid = bool.TryParse(input, out var result);
            if (valid)
                return result;
            if (throwExceptionIfFailed)
            {
                throw new FormatException($"'{input}' cannot be converted as bool");
            }
            return null;
        }

        /// <summary>
        /// returns if string can parse to byte
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableByte(this string stringValue)
        {
            return byte.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns if string can parse to integer
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableInt(this string stringValue)
        {
            return int.TryParse(stringValue, out _);
        }
        
        /// <summary>
        /// returns if string can parse to bool
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableBool(this string stringValue)
        {
            return bool.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns if string can parse to long
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableLong(this string stringValue)
        {
            return long.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns if string can parse to double
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableDouble(this string stringValue)
        {
            return double.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns if string can parse to float
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableFloat(this string stringValue)
        {
            return float.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns if string can parse to decimal
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static bool IsParsableDecimal(this string stringValue)
        {
            return decimal.TryParse(stringValue, out _);
        }

        /// <summary>
        /// returns the string with replacements for german umlauts
        /// </summary>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static string ReplaceUmlauts(this string stringValue)
        {
            return stringValue.Aggregate(new StringBuilder(), (stringBuilder, chr) => UmlautMapping.TryGetValue(chr, out var result) ? stringBuilder.Append(result) : stringBuilder.Append(chr)).ToString();
        }

        /// <summary>
        /// Treats the input as query string and adds key and value as query parameter
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AppendQueryParameter(this string input, string key, string value)
        {
            var param = $"{key}={value}";
            return input.AppendQueryParameter(param);
        }

        /// <summary>
        /// Treats the input as query string and appends a parameter
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AppendQueryParameter(this string input, string value)
        {
            if (input.Contains("?"))
            {
                return input + "&" + value;
            }
            return input + "?" + value;
        }

        /// <summary>
        /// Method which split a string like 'EventName' into 'Event.Name' or by any other delimiter provided.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string SplitCamelCase(this string input, char delimiter = '.')
        {
            return string.IsNullOrEmpty(input) ? input : Regex.Replace(input, "([A-Z])", delimiter + "$1", RegexOptions.Compiled).TrimStart(delimiter);
        }

        /// <summary>
        /// Convert string to camel case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string input)
        {
            // If there are 0 or 1 characters, just return the string.
            if (input == null || input.Length < 2)
                return input;

            // Split the string into words.
            var words = input.Split(
                new[] { ' ', '-', '_' },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            var result = words[0].ToLower();
            for (var i = 1; i < words.Length; i++)
            {
                result +=
                    words[i].Substring(0, 1).ToUpper() +
                    words[i].Substring(1);
            }

            return result;
        }

        /// <summary>
        /// Encrypts the given string using AES encryption. See <see cref="Decrypt"/> method.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(this string text, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("The text must have valid value.", nameof(text));

            var buffer = Encoding.UTF8.GetBytes(text);
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(buffer))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    var result = resultStream.ToArray();
                    var combined = new byte[aes.IV.Length + result.Length];
                    Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
                    Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                    return Convert.ToBase64String(combined);
                }
            }
        }

        /// <summary>
        /// Decrypts a string which was encrypted with AES. See <see cref="Encrypt"/> method.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(this string encryptedText, string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key must have valid value.", nameof(key));
            if (string.IsNullOrEmpty(encryptedText))
                throw new ArgumentException("The encrypted text must have valid value.", nameof(encryptedText));

            var combined = Convert.FromBase64String(encryptedText);
            var buffer = new byte[combined.Length];
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                var iv = new byte[aes.IV.Length];
                var ciphertext = new byte[buffer.Length - iv.Length];

                Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
                Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(ciphertext))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    return Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
        }

        /// <summary>
        /// Convert the given string to a value of the given enum type
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum? AsEnum<TEnum>(this string name) where TEnum : struct
        {
            if (Enum.TryParse(name, out TEnum entityType))
            {
                return entityType;
            }
            return null;
        }
    }
}
