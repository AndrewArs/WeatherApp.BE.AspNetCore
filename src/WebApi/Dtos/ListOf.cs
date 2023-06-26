namespace WebApi.Dtos;

public record ListOfDto<T>(ICollection<T> Data);