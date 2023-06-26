using BenchmarkDotNet.Attributes;
using Infrastructure.Services;

namespace Benchmarks.Benchmarks;

public class TemplateServiceBenchmarks
{
    private readonly TemplateService _templateService;

    public TemplateServiceBenchmarks()
    {
        _templateService = new TemplateService(new JsonParserService());
    }

    public IEnumerable<object> Templates()
    {
        yield return "My custom weather template {{template_name}}. Humidity level: {{advanced.humidity.level}}; Temperature: {{temperature}}; Wind speed: {{advanced.wind_speed}}.";
        yield return Enumerable.Repeat("some {{param1}} ", 100).Aggregate((x, y) => $"{x} {y}");
        yield return Enumerable.Repeat("{{param1", 1000).Aggregate((x, y) => $"{x} {y}");
        yield return Enumerable.Repeat("text", 100).Concat(Enumerable.Repeat("{{text}}", 1000)).Concat(Enumerable.Repeat(" {{text}}", 10000)).Aggregate((x, y) => $"{x} {y}");
    }

    [Benchmark]
    [ArgumentsSource(nameof(Templates))]
    public IEnumerable<string> GetAllPlaceholders(string template)
    {
        for (var i = 0; i < template.Length;)
        {
            var start = template[i..].IndexOf("{{", StringComparison.InvariantCulture);

            if (start == -1)
            {
                yield break;
            }

            start += i;

            var tokenSize = template[start..].IndexOf("}}", StringComparison.InvariantCulture);

            if (tokenSize == -1)
            {
                yield break;
            }

            yield return template.Substring(start + 2, tokenSize - 2);

            i = start + tokenSize + 2;
        }
    }

    [Benchmark]
    [ArgumentsSource(nameof(Templates))]
    public IEnumerable<Memory<char>> GetAllPlaceholdersAsMemory(string template)
    {
        var charArray = template.ToCharArray();
        for (var i = 0; i < template.Length;)
        {
            var start = template[i..].IndexOf("{{", StringComparison.InvariantCulture);

            if (start == -1)
            {
                yield break;
            }

            start += i;

            var tokenSize = template[start..].IndexOf("}}", StringComparison.InvariantCulture);

            if (tokenSize == -1)
            {
                yield break;
            }

            yield return new Memory<char>(charArray, start + 2, tokenSize - 2);

            i = start + tokenSize + 2;
        }
    }
}