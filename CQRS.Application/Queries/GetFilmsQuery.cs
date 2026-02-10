using CQRS.Shared.Enums;

namespace CQRS.Application.Queries;

public class GetFilmsQuery 
{
    public Guid RealisateurId { get; private set; }
    
    public int PageNumber { get; private set; } 
    
    public int PageSize { get; private set; }
    
    public FilmSortBy SortBy { get; private set; }
    
    public SortDirection SortDirection { get; private set; } 

    public GetFilmsQuery(Guid realisateurId, int pageNumber, int pageSize, FilmSortBy sortBy, SortDirection sortDirection)
    {
        RealisateurId = realisateurId;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortBy = sortBy;
        SortDirection = sortDirection;
    }
}