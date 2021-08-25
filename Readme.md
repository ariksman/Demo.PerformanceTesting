# Solution structure

Initial idea for benchmarking is from Nick Chapsas video: https://youtu.be/BoE5Y6Xkm6w

The `Enumeration` base class is based on this article: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
```
.
└── Demo.EnumVsEnumeration.BenchmarkTests
```

## Packages in use

- Benchmark - <https://github.com/dotnet/BenchmarkDotNet>


## Results

```
|                           Method |          Mean |      Error |     StdDev |  Gen 0 | Allocated |
|--------------------------------- |--------------:|-----------:|-----------:|-------:|----------:|
|          NativeEnumValueToString |    31.1550 ns |  0.6437 ns |  0.6322 ns | 0.0038 |      24 B |
|      NativeEnumAllValuesToString | 1,201.1722 ns | 24.0286 ns | 33.6848 ns | 0.0782 |     496 B |
|         EnumerationValueToString |     0.0000 ns |  0.0000 ns |  0.0000 ns |      - |         - |
| EnumerationListAllValuesToString |   125.7677 ns |  2.5274 ns |  4.1526 ns | 0.0433 |     272 B |
|  EnumerationGetAllValuesToString |   519.0227 ns | 10.3592 ns | 11.5142 ns | 0.0544 |     344 B |

```

Its worthwhile to notice that a type based on `Enumeration` can contain pre-assigned display name, and therefore the conversion does  not allocate any memory when accessing this name.