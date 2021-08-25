# Solution structure

Initial idea from https://youtu.be/BoE5Y6Xkm6w
```
.
└── Demo.EnumVsEnumeration.BenchmarkTests
```

## Packages in use

- Benchmark - <https://github.com/dotnet/BenchmarkDotNet>


## Results

```
|                       Method |          Mean |      Error |     StdDev |        Median |  Gen 0 | Allocated |
|----------------------------- |--------------:|-----------:|-----------:|--------------:|-------:|----------:|
|      NativeEnumValueToString |    32.8070 ns |  0.6662 ns |  1.3459 ns |    32.4304 ns | 0.0038 |      24 B |
|  NativeEnumAllValuesToString | 1,185.2035 ns | 19.6268 ns | 18.3589 ns | 1,182.6727 ns | 0.0782 |     496 B |
|     EnumerationValueToString |     0.0030 ns |  0.0083 ns |  0.0085 ns |     0.0000 ns |      - |         - |
| EnumerationAllValuesToString |   123.1986 ns |  1.9748 ns |  1.8472 ns |   123.5370 ns | 0.0433 |     272 B |
```

Its worthwhile to notice that the `Enumeration` can contain pre-assigned display name, and therefore the conversion does  not allocate any memory when accessing this name.