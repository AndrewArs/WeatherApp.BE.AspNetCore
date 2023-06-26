namespace Application.Common.Models;

public class ListOf<T>
{
    public ICollection<T> Data { get; set; }

    public ListOf(ICollection<T> data)
    {
        Data = data;
    }
}