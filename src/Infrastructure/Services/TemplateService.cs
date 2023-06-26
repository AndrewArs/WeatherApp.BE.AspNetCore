using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class TemplateService : ITemplateService
{
    private readonly IJsonParserService _jsonParserService;

    public TemplateService(IJsonParserService jsonParserService)
    {
        _jsonParserService = jsonParserService;
    }

    private static class Token
    {
        public const string LeftBrackets = "{{";
        public const string RightBrackets = "}}";
    }

    public IEnumerable<string> GetAllPlaceholders(string template)
    {
        for (var i = 0; i < template.Length;)
        {
            var start = template[i..].IndexOf(Token.LeftBrackets, StringComparison.InvariantCulture);

            if (start == -1)
            {
                yield break;
            }

            start += i;

            var tokenSize = template[start..].IndexOf(Token.RightBrackets, StringComparison.InvariantCulture);

            if (tokenSize == -1)
            {
                yield break;
            }

            yield return template.Substring(start + 2, tokenSize - 2);

            i = start + tokenSize + 2;
        }
    }

    public IEnumerable<string> GetAllPlaceholdersWithBrackets(string template)
    {
        for (var i = 0; i < template.Length;)
        {
            var start = template[i..].IndexOf(Token.LeftBrackets, StringComparison.InvariantCulture);

            if (start == -1)
            {
                yield break;
            }

            start += i;

            var tokenSize = template[start..].IndexOf(Token.RightBrackets, StringComparison.InvariantCulture);

            if (tokenSize == -1)
            {
                yield break;
            }

            yield return template.Substring(start, tokenSize + 2);

            i = start + tokenSize + 2;
        }
    }

    public string BuildTemplateFromJson(string template, string json)
    {
        if (template is null) throw new ArgumentNullException(nameof(template));
        if (json is null) throw new ArgumentNullException(nameof(json));

        var tokens = GetAllPlaceholdersWithBrackets(template)
            .Distinct()
            .ToDictionary(x => x, x => _jsonParserService.GetValueByPath<object>(json, x[2..^2]).ToString());

        return tokens.Keys.Aggregate(template, (current, token) => current.Replace(token, tokens[token]));
    }
}