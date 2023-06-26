using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Benchmarks.Benchmarks;

//var summary = BenchmarkRunner.Run<TemplateServiceBenchmarks>();
var summary = BenchmarkRunner.Run<BuildTemplateBenchmarks>(DefaultConfig.Instance
    .AddDiagnoser(MemoryDiagnoser.Default));
