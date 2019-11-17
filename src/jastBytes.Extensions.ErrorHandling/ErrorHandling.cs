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

namespace jastBytes.Extensions.ErrorHandling
{
    public static class ErrorHandling
    {
        public static Exception Err(Action action)
        {
            try
            {
                action.Invoke();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static async Task<Exception> ErrAsync(Func<Task> func)
        {
            try
            {
                await func.Invoke();
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public static (TResult, Exception) Err<TResult>(Func<TResult> func)
        {
            try
            {
                return (func.Invoke(), null);
            }
            catch (Exception ex)
            {
                return (default, ex);
            }
        }

        public static async Task<(TResult, Exception)> ErrAsync<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return (await func.Invoke(), null);
            }
            catch (Exception ex)
            {
                return (default, ex);
            }
        }

        public static (TResult, TException) Err<TResult, TException>(Func<TResult> func) where TException : Exception
        {
            try
            {
                return (func.Invoke(), null);
            }
            catch (TException ex)
            {
                return (default, ex);
            }
        }

        public static async Task<(TResult, TException)> ErrAsync<TResult, TException>(Func<Task<TResult>> func) where TException : Exception
        {
            try
            {
                return (await func.Invoke(), null);
            }
            catch (TException ex)
            {
                return (default, ex);
            }
        }
    }
}
