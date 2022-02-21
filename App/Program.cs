using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;

var result = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll();

// output all results again, nicely together
foreach (var summary in result)
{
    MarkdownExporter.Console.ExportToLog(summary, ConsoleLogger.Default);
}