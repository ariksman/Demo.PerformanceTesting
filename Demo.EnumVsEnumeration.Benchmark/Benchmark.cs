using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;

namespace Demo.EnumVsEnumeration.Benchmark
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        [Benchmark]
        public string NativeEnumValueToString()
        {
            return TestStatusEnum.Valid.ToString();
        }

        [Benchmark]
        public List<string> NativeEnumAllValuesToString()
        {
            var values = new List<string>();
            foreach (var value in Enum.GetValues(typeof(TestStatusEnum)))
            {
                values.Add(value.ToString() ?? string.Empty);
            }

            return values;
        }
        
        [Benchmark]
        public string EnumerationValueToString()
        {
            return TestStatusEnumeration.Valid.Name;
        }

        [Benchmark]
        public List<string> EnumerationListAllValuesToString()
        {
            var values = new List<string>();
            foreach (var value in TestStatusEnumeration.List())
            {
                values.Add(value.Name);
            }

            return values;
        }
        
        [Benchmark]
        public List<string> EnumerationGetAllValuesToString()
        {
            var values = new List<string>();
            foreach (var value in Enumeration.GetAll<TestStatusEnumeration>())
            {
                values.Add(value.Name);
            }

            return values;
        }
    }
}