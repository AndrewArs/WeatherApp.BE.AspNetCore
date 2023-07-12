using Application.Common.Interfaces;
using Humanizer;
using System.Globalization;
using System.Text;

namespace Infrastructure.Services;
public class SlugService : ISlugService
{
    public string? Slugify(string? value) => value is null ? value : RemoveDiacritics(value!).Dehumanize().Kebaberize();

    private string RemoveDiacritics(string value)
    {
        var stFormD = value.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();

        foreach (var t in stFormD)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(t);
            if (uc != UnicodeCategory.NonSpacingMark) sb.Append(t);
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
