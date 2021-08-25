using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.EnumVsEnumeration.Benchmark
{
    public class TestStatusEnumeration : Enumeration
    {
        public static TestStatusEnumeration Invalid = new TestStatusEnumeration(1, nameof(Invalid).ToLowerInvariant());
        public static TestStatusEnumeration Valid = new TestStatusEnumeration(2, nameof(Valid).ToLowerInvariant());
        public static TestStatusEnumeration Normal = new TestStatusEnumeration(3, nameof(Normal).ToLowerInvariant());
        public static TestStatusEnumeration Average = new TestStatusEnumeration(4, nameof(Average).ToLowerInvariant());
        public static TestStatusEnumeration High = new TestStatusEnumeration(5, nameof(High).ToLowerInvariant());

        public TestStatusEnumeration(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<TestStatusEnumeration> List() =>
            new[] { Invalid, Valid, Normal, Average, High };

        public static TestStatusEnumeration FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state is null)
            {
                throw new ApplicationException($"Possible values for status: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TestStatusEnumeration From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ApplicationException($"Possible values for status: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}