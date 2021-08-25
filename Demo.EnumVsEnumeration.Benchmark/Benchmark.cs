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
            return TestEnum.Valid.ToString();
        }

        [Benchmark]
        public List<string> NativeEnumAllValuesToString()
        {
            var values = new List<string>();
            foreach (var value in Enum.GetValues(typeof(TestEnum)))
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
        public List<string> EnumerationAllValuesToString()
        {
            var values = new List<string>();
            foreach (var value in TestStatusEnumeration.List())
            {
                values.Add(value.Name);
            }

            return values;
        }
    }
}