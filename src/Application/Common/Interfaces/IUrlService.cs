namespace Application.Common.Interfaces;

public interface IUrlService
{
    string Mask { get; set; }
    string MaskUrlQueryParams(string url, params string[] queryParams);
}