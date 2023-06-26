using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Application.Common.Interfaces;

namespace Infrastructure.Services;
public class JsonParserService : IJsonParserService
{
    private readonly Regex _indexRegex = new(@"^\[\d+\]$", RegexOptions.Compiled);
    private readonly Regex _propertyRegex = new(@"^[A-Za-z_]{1}\w+$", RegexOptions.Compiled);

    private enum PathSegmentType : byte
    {
        Undefined = 0,
        Index = 1,
        Property = 2,
    }

    public bool IsValidPath(string path) 
        => path.Split(".").Select(GetPathSegmentType).All(x => x != PathSegmentType.Undefined);

    public TValue GetValueByPath<TValue>(string json, string path)
    {
        var jsonNode = JsonNode.Parse(json);

        foreach (var pathItem in path.Split("."))
        {
            if (jsonNode == null)
            {
                throw new ArgumentException("Cannot parse json", nameof(json));
            }

            var type = GetPathSegmentType(pathItem);

            jsonNode = type switch
            {
                PathSegmentType.Property => jsonNode.AsObject()
                    .FirstOrDefault(x => string.Equals(x.Key, pathItem, StringComparison.CurrentCultureIgnoreCase))
                    .Value,
                PathSegmentType.Index => jsonNode.AsArray()[int.Parse(pathItem[1..^1])],
                _ => throw new ArgumentException("Invalid path", nameof(path))
            };
        }

        if (jsonNode == null)
        {
            throw new ArgumentException("Cannot parse json", nameof(json));
        }

        return jsonNode.GetValue<TValue>();
    }


    private PathSegmentType GetPathSegmentType(string pathSegment)
    {
        return pathSegment switch
        {
            _ when _indexRegex.IsMatch(pathSegment) => PathSegmentType.Index,
            _ when _propertyRegex.IsMatch(pathSegment) => PathSegmentType.Property,
            _ => PathSegmentType.Undefined
        };
    }
}
