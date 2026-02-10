namespace CQRS.Application.Common;

public class PagedResult<T>
{
    public int TotalItems { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
    
    public List<T> Items { get; set; } = new List<T>();
}