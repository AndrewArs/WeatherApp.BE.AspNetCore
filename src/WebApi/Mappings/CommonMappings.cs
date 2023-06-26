namespace WebApi.Mappings;

public static class CommonMappings
{
    public static ErrorDto ToDto(this Error error) => new(error.Message ?? "Error");
}