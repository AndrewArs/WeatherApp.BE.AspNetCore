using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class UrlService : IUrlService
{
    public string Mask { get; set; } = "{secret}";

    public string MaskUrlQueryParams(string url, params string[] queryParams)
    {
        var tempUrl = url;
        if (!url.StartsWith("http") || !url.StartsWith("https"))
        {
            tempUrl = $"http://{url}";
        }
        if (!Uri.TryCreate(tempUrl, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("Invalid Url", nameof(url));
        }

        // skip '?'
        var oldQuery = uri.Query[1..];
        var query = oldQuery.Split("&");

        var newQuery = query.Select(x =>
        {
            if (queryParams.Any(x.StartsWith))
            {
                var i = x.IndexOf('=') + 1;
                return $"{x[..i]}{Mask}";
            }

            return x;
        }).Aggregate((q1, q2) => $"{q1}&{q2}");

        return url.Replace(oldQuery, newQuery);
    }
}