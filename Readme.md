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
|                           Method |          Mean |      Error |     StdDev |        Median |  Gen 0 | Allocated |
|--------------------------------- |--------------:|-----------:|-----------:|--------------:|-------:|----------:|
|          NativeEnumValueToString |    32.1648 ns |  0.6373 ns |  0.5649 ns |    32.0189 ns | 0.0038 |      24 B |
|      NativeEnumAllValuesToString | 1,236.8316 ns | 23.4125 ns | 36.4505 ns | 1,232.6040 ns | 0.0782 |     496 B |
|         EnumerationValueToString |     0.0081 ns |  0.0210 ns |  0.0288 ns |     0.0000 ns |      - |         - |
| EnumerationListAllValuesToString |   131.7649 ns |  2.6642 ns |  5.8479 ns |   130.0606 ns | 0.0433 |     272 B |
|  EnumerationGetAllValuesToString |   524.3484 ns | 10.4138 ns | 14.9352 ns |   518.5165 ns | 0.0544 |     344 B |

```

Its worthwhile to notice that a type based on `Enumeration` can contain pre-assigned display name, and therefore the conversion does  not allocate any memory when accessing this name.

The native `Enum` has to execute binary search in order to find out the correct enum value which can be O (log n).
```c#

public override string ToString()
{
    // Returns the value in a human readable format.  For PASCAL style enums who's value maps directly the name of the field is returned.
    // For PASCAL style enums who's values do not map directly the decimal value of the field is returned.
    // For BitFlags (indicated by the Flags custom attribute): If for each bit that is set in the value there is a corresponding constant
    // (a pure power of 2), then the OR string (ie "Red, Yellow") is returned. Otherwise, if the value is zero or if you can't create a string that consists of
    // pure powers of 2 OR-ed together, you return a hex value

    // Try to see if its one of the enum values, then we return a String back else the value
    return InternalFormat((RuntimeType)GetType(), ToUInt64()) ?? ValueToString();
}
        
private static string? InternalFormat(RuntimeType enumType, ulong value)
{
    EnumInfo enumInfo = GetEnumInfo(enumType);

    if (!enumInfo.HasFlagsAttribute)
    {
        return GetEnumName(enumInfo, value);
    }
    else // These are flags OR'ed together (We treat everything as unsigned types)
    {
        return InternalFlagsFormat(enumType, enumInfo, value);
    }
}
        
private static string? GetEnumName(EnumInfo enumInfo, ulong ulValue)
{
    int index = Array.BinarySearch(enumInfo.Values, ulValue);
    if (index >= 0)
    {
        return enumInfo.Names[index];
    }

    return null; // return null so the caller knows to .ToString() the input
}
```
