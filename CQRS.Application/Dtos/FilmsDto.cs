namespace CQRS.Application.Dtos;

public class FilmsDto
{
    public int TotalItems { get; set; }
    
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
    
    public List<FilmDto> Films { get; set; }
}