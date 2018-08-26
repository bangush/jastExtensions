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
using System.Linq;

namespace System.Collections.Generic
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Iterates through the collection and invokes every element by the given selector
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        public static void ForEach<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            foreach (var item in source)
            {
                selector.Invoke(item);
            }
        }

        /// <summary>
        /// Iterates through the collection and invokes the action on every element
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var forEach = source as TSource[] ?? source.ToArray();
            foreach (var item in forEach)
            {
                action.Invoke(item);
            }

            return forEach;
        }

        /// <summary>
        /// Removes all elements from the collection which are null 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(t => t != null);
        }

        /// <summary>
        /// Removes every entry from the dictionary whose value is null
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ValueNotNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Where(pair => pair.Value != null).ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}