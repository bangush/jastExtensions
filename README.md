[![Build status](https://ci.appveyor.com/api/projects/status/e9td2y2war68130e?svg=true)](https://ci.appveyor.com/project/iss0/jastextensions)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](http://opensource.org/licenses/MIT)

| Package Name                  | Status                                   |
|-------------------------------|------------------------------------------|
| jastBytes.Extensions.String   | [![NuGet version](https://badge.fury.io/nu/jastBytes.Extensions.String.svg)](https://badge.fury.io/nu/jastBytes.Extensions.String) |
| jastBytes.Extensions.DateTime | [![NuGet version](https://badge.fury.io/nu/jastBytes.Extensions.DateTime.svg)](https://badge.fury.io/nu/jastBytes.Extensions.DateTime) |
| jastBytes.Extensions.DirectoryInfo | [![NuGet version](https://badge.fury.io/nu/jastBytes.Extensions.DirectoryInfo.svg)](https://badge.fury.io/nu/jastBytes.Extensions.DirectoryInfo) |
| jastBytes.Extensions.Linq | [![NuGet version](https://badge.fury.io/nu/jastBytes.Extensions.DirectoryInfo.svg)](https://badge.fury.io/nu/jastBytes.Extensions.Linq) |

# jastExtensions
A library of .NET C# extension methods for .NETFramework >=4.5 and .NETStandard >=2.0.

## Examples

### using jastBytes.Extensions.String
 ```csharp
// String encryption and decryption with AES
const string text = "Hello World!";
const string secretKey = "secret-string-to-encrypt";

var encryptedString = text.Encrypt(secretKey);
var decryptedString = encryptedString.Decrypt(secretKey);
```
```csharp
// String conversion
const string boolString = "true";
const string intString = "1337";
bool boolResult = boolString.ToBool();
int inResult = boolString.ToInt();

cosnt string testString = "hello world"
var camelCase = testString.ToCamelCase(); // helloWorld
```