using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace jastBytes.Extensions.Tests
{
    public class LinqExtensionsShould
    {
        [Fact]
        public void ForEach()
        {
            var listOfExecuted = new List<int>();
            var enumerable = Enumerable.Range(0, 1000);

            var enumerated = enumerable
                .Skip(1)
                .Take(2)
                .ForEach(i => listOfExecuted.Add(i))
                .ToList();

            Assert.True(listOfExecuted.Count == 2);
            Assert.Equal(enumerated, listOfExecuted);
        }
    }
}
