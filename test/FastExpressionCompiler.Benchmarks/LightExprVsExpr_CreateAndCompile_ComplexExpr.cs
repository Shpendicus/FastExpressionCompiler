﻿using BenchmarkDotNet.Attributes;
using FastExpressionCompiler.LightExpression.UnitTests;
using LE = FastExpressionCompiler.LightExpression.ExpressionCompiler;

namespace FastExpressionCompiler.Benchmarks
{
    [MemoryDiagnoser]
    public class LightExprVsExpr_CreateAndCompile_ComplexExpr
    {
        /**
        ## 25.01.2019
         
        BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17134.523 (1803/April2018Update/Redstone4)
        Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
        Frequency=2156255 Hz, Resolution=463.7670 ns, Timer=TSC
        .NET Core SDK=2.2.100
          [Host]     : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
          DefaultJob : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT


                                        Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
        -------------------------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
         CreateLightExpression_and_CompileFast |  12.68 us | 0.0555 us | 0.0492 us |  1.00 |    0.00 |      1.4343 |      0.7172 |      0.0458 |             6.61 KB |
              CreateExpression_and_CompileFast |  19.26 us | 0.2559 us | 0.2268 us |  1.52 |    0.02 |      1.5564 |      0.7629 |      0.0305 |             7.23 KB |
                  CreateExpression_and_Compile | 260.67 us | 1.7431 us | 1.6305 us | 20.54 |    0.14 |      1.4648 |      0.4883 |           - |             7.16 KB |

        ## v2.1
                                        Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
        -------------------------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
         CreateLightExpression_and_CompileFast |  11.82 us | 0.0735 us | 0.0652 us |  1.00 |    0.00 |      1.5259 |      0.7629 |      0.0458 |             7.02 KB |
              CreateExpression_and_CompileFast |  17.41 us | 0.2181 us | 0.2040 us |  1.47 |    0.02 |      1.5259 |      0.7629 |      0.0305 |             7.11 KB |
                  CreateExpression_and_Compile | 239.20 us | 1.5860 us | 1.4835 us | 20.24 |    0.15 |      1.4648 |      0.7324 |           - |             7.16 KB |

        ## v3.0-preview-02

        |                                Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |-------------------------------------- |----------:|---------:|---------:|------:|--------:|-------:|-------:|-------:|----------:|
        |          CreateExpression_and_Compile | 241.97 us | 2.007 us | 1.877 us | 17.77 |    0.20 | 1.7090 | 0.7324 |      - |   7.01 KB |
        |      CreateExpression_and_CompileFast |  17.30 us | 0.207 us | 0.173 us |  1.27 |    0.02 | 1.7395 | 0.8545 | 0.0305 |   7.19 KB |
        | CreateLightExpression_and_CompileFast |  13.61 us | 0.158 us | 0.140 us |  1.00 |    0.00 | 1.6174 | 0.7935 | 0.0305 |   6.64 KB |

        ## v3.0-preview-05

        BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
        Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
        .NET Core SDK=5.0.100
        [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
        DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

        |                                Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |-------------------------------------- |----------:|---------:|---------:|------:|--------:|-------:|-------:|-------:|----------:|
        |          CreateExpression_and_Compile | 244.61 us | 4.700 us | 6.111 us | 17.92 |    0.50 | 1.7090 | 0.7324 |      - |    7.2 KB |
        |      CreateExpression_and_CompileFast |  17.69 us | 0.350 us | 0.443 us |  1.31 |    0.04 | 1.8005 | 0.8850 | 0.0305 |   7.36 KB |
        | CreateLightExpression_and_CompileFast |  13.50 us | 0.152 us | 0.143 us |  1.00 |    0.00 | 1.5869 | 0.7935 | 0.0305 |   6.58 KB |

        ## v3.1

        BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
        Intel Core i9-8950HK CPU 2.90GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
        .NET Core SDK=5.0.202
        [Host]     : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT
        DefaultJob : .NET Core 5.0.5 (CoreCLR 5.0.521.16609, CoreFX 5.0.521.16609), X64 RyuJIT


        |                                Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |-------------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|-------:|-------:|----------:|
        |          CreateExpression_and_Compile | 185.081 us | 3.5893 us | 3.8406 us | 21.98 |    0.62 | 0.9766 | 0.4883 |      - |    7.2 KB |
        |      CreateExpression_and_CompileFast |  11.538 us | 0.2286 us | 0.6411 us |  1.41 |    0.10 | 1.0223 | 0.5035 | 0.0458 |   6.28 KB |
        | CreateLightExpression_and_CompileFast |   8.408 us | 0.1501 us | 0.1844 us |  1.00 |    0.00 | 0.8850 | 0.4425 | 0.0458 |   5.47 KB |

        ## v3.2

        |                                Method |       Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |-------------------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|-------:|-------:|----------:|
        |          CreateExpression_and_Compile | 239.100 us | 3.4564 us | 3.2331 us | 25.78 |    0.54 | 0.9766 | 0.4883 |      - |   7.19 KB |
        |      CreateExpression_and_CompileFast |  13.480 us | 0.2612 us | 0.2443 us |  1.45 |    0.02 | 1.0071 | 0.4883 | 0.0305 |   6.28 KB |
        | CreateLightExpression_and_CompileFast |   9.278 us | 0.1465 us | 0.1370 us |  1.00 |    0.00 | 0.8698 | 0.4272 | 0.0305 |   5.41 KB |

        ## v3.3 with net6

        BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19043
        Intel Core i9-8950HK CPU 2.90GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
        .NET Core SDK=6.0.201
        [Host]     : .NET Core 6.0.3 (CoreCLR 6.0.322.12309, CoreFX 6.0.322.12309), X64 RyuJIT
        DefaultJob : .NET Core 6.0.3 (CoreCLR 6.0.322.12309, CoreFX 6.0.322.12309), X64 RyuJIT

        |                                Method |      Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |-------------------------------------- |----------:|---------:|---------:|------:|--------:|-------:|-------:|-------:|----------:|
        |          CreateExpression_and_Compile | 287.35 us | 5.465 us | 6.712 us | 23.80 |    0.76 | 0.9766 | 0.4883 |      - |   7.25 KB |
        |      CreateExpression_and_CompileFast |  16.02 us | 0.261 us | 0.232 us |  1.32 |    0.03 | 1.0376 | 0.5188 | 0.0305 |   6.53 KB |
        | CreateLightExpression_and_CompileFast |  12.07 us | 0.228 us | 0.272 us |  1.00 |    0.00 | 0.9003 | 0.4425 | 0.0305 |   5.58 KB |

        |                                           Method |     Mean |     Error |    StdDev | Ratio |  Gen 0 |  Gen 1 |  Gen 2 | Allocated |
        |------------------------------------------------- |---------:|----------:|----------:|------:|-------:|-------:|-------:|----------:|
        |            CreateLightExpression_and_CompileFast | 9.368 us | 0.1285 us | 0.1139 us |  1.00 | 0.9003 | 0.4425 | 0.0458 |   5.58 KB |
        | CreateLightExpression_and_CompileFast_NoByRefNew | 9.372 us | 0.1429 us | 0.1267 us |  1.00 | 0.9003 | 0.4425 | 0.0305 |   5.56 KB |        */

        // [Benchmark]
        public object CreateExpression_and_Compile() =>
            LightExpressionTests.CreateComplexExpression().Compile();

        // [Benchmark]
        public object CreateExpression_and_CompileFast() =>
            LightExpressionTests.CreateComplexExpression().CompileFast();

        [Benchmark(Baseline = true)]
        public object CreateLightExpression_and_CompileFast() =>
            LE.CompileFast(LightExpressionTests.CreateComplexLightExpression());

        [Benchmark]
        public object CreateLightExpression_and_CompileFast_NoByRefNew() =>
            LE.CompileFast(LightExpressionTests.CreateComplexLightExpression_with_tricks());
    }

    [MemoryDiagnoser]
    public class LightExprVsExpr_Create_ComplexExpr
    {
        /*
        ## V2 baseline, 25.01.2019

                       Method  |       Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
        ---------------------- |-----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
             CreateExpression  | 3,574.7 ns | 8.0032 ns | 7.4862 ns |  9.18 |    0.02 |      0.2823 |           - |           - |              1344 B |
         CreateLightExpression |   389.5 ns | 0.9547 ns | 0.7972 ns |  1.00 |    0.00 |      0.1693 |           - |           - |               800 B |

        ## V3-preview-01

        |                Method |       Mean |    Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |---------------------- |-----------:|---------:|----------:|------:|--------:|-------:|------:|------:|----------:|
        |      CreateExpression | 2,805.2 ns | 55.57 ns | 107.06 ns |  4.76 |    0.32 | 0.3090 |     - |     - |    1304 B |
        | CreateLightExpression |   578.5 ns |  6.39 ns |   5.98 ns |  1.00 |    0.00 | 0.1678 |     - |     - |     704 B |

        # V3-preview-02

        |                Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |---------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
        |      CreateExpression | 3,126.9 ns | 62.14 ns | 80.80 ns |  5.46 |    0.13 | 0.3052 |     - |     - |    1304 B |
        | CreateLightExpression |   578.4 ns |  8.07 ns |  7.54 ns |  1.00 |    0.00 | 0.1640 |     - |     - |     688 B |

        # V3-preview-03

        |                Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |---------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
        |      CreateExpression | 3,351.7 ns | 59.81 ns | 55.94 ns | 10.03 |    0.23 | 0.3090 |     - |     - |    1304 B |
        | CreateLightExpression |   334.3 ns |  4.85 ns |  4.53 ns |  1.00 |    0.00 | 0.1316 |     - |     - |     552 B |

        # V3-preview-05

        BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.630 (2004/?/20H1)
        Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
        .NET Core SDK=5.0.100
        [Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
        DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT


        |                Method |       Mean |    Error |   StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |---------------------- |-----------:|---------:|---------:|------:|--------:|-------:|------:|------:|----------:|
        |      CreateExpression | 2,508.2 ns | 44.12 ns | 36.84 ns |  8.83 |    0.14 | 0.3128 |     - |     - |    1312 B |
        | CreateLightExpression |   284.2 ns |  5.19 ns |  4.85 ns |  1.00 |    0.00 | 0.1316 |     - |     - |     552 B |

        */
        [Benchmark(Baseline = true)]
        public object CreateLightExpression() =>
            LightExpressionTests.CreateComplexLightExpression();

        [Benchmark]
        public object CreateExpression() =>
            LightExpressionTests.CreateComplexExpression();
    }
}
