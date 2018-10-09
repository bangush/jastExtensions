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
bool boolResult = "true".ToBool();
int intResult = "1337".ToInt();

var camelCase = "hello world".ToCamelCase(); // camelCase = "helloWorld"
```

```csharp
// Linq foreach
var listOfExecuted = new List<int>();

Enumerable
    .Range(0, 10)
    .Skip(1)
    .Take(2)
    .ForEachInvoke(i => listOfExecuted.Add(i)); // enumerate and invoke

var enumerable = Enumerable
    .Range(0, 10)
    .Skip(1)
    .Take(2)
    .ForEach(i => Console.WriteLine(i)); // not enumerated yet, no console output

var list = enumerable.ToList(); // list contains { 1, 2 } and these two numbers are printed on console

// listOfExecuted = { 1, 2 }
```