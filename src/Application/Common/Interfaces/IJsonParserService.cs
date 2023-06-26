namespace Application.Common.Interfaces;

public interface IJsonParserService
{
    bool IsValidPath(string path);
    TValue GetValueByPath<TValue>(string json, string path);
}