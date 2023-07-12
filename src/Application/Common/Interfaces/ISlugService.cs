namespace Application.Common.Interfaces;

public interface ISlugService
{
    string? Slugify(string? value);
}