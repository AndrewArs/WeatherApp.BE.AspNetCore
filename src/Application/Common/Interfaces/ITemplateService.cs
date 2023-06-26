namespace Application.Common.Interfaces;

public interface ITemplateService
{
    IEnumerable<string> GetAllPlaceholders(string template);
    IEnumerable<string> GetAllPlaceholdersWithBrackets(string template);
    string BuildTemplateFromJson(string template, string json);
}