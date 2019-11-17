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

using System;
using System.Threading.Tasks;
using Xunit;
using static jastBytes.Extensions.ErrorHandling.ErrorHandling;

namespace jastBytes.Extensions.Tests
{
    public class ErrorHandlingExtensionsShould
    {
        [Fact]
        public void HandleExceptionWithReturn()
        {
            var (str, err) = Err(() => CanErrorWithResult(true));
            Assert.Null(str);
            Assert.NotNull(err);

            (str, err) = Err(() => CanErrorWithResult(false));
            Assert.NotNull(str);
            Assert.Null(err);

            Assert.Throws<Exception>(() => Err<string, ArgumentException>(() => CanErrorWithResult(true)));
        }

        [Fact]
        public async Task HandleExceptionWithReturnAsync()
        {
            var (str, err) = await ErrAsync(() => CanErrorWithResultAsync(true));
            Assert.Null(str);
            Assert.NotNull(err);

            await Assert.ThrowsAsync<Exception>(() => ErrAsync<string, ArgumentException>(() => CanErrorWithResultAsync(true)));
        }

        [Fact]
        public void HandleExceptionWithoutReturn()
        {
            var err = Err(() => CanErrorNoResult(true));
            Assert.NotNull(err);

            err = Err(() => CanErrorNoResult(false));
            Assert.Null(err);
        }

        [Fact]
        public async Task HandleExceptionWithoutReturnAsync()
        {
            var err = await ErrAsync(() => CanErrorNoResultAsync(true));
            Assert.NotNull(err);

            err = await ErrAsync(() => CanErrorNoResultAsync(false));
            Assert.Null(err);
        }

        private static async Task CanErrorNoResultAsync(bool shouldThrow)
        {
            await Task.Factory.StartNew(() => CanErrorNoResult(shouldThrow));
        }

        private static async Task<string> CanErrorWithResultAsync(bool shouldThrow)
        {
            return await Task.Factory.StartNew(() => CanErrorWithResult(shouldThrow));
        }

        private static void CanErrorNoResult(bool shouldThrow)
        {
            if (shouldThrow) throw new Exception("Oh snap!");
        }

        private static string CanErrorWithResult(bool shouldThrow)
        {
            if (shouldThrow) throw new Exception("Oh snap!");
            return "Hello World";
        }
    }
}
