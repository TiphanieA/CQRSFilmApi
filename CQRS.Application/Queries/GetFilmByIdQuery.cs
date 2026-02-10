
namespace CQRS.Application.Queries;

public class GetFilmByIdQuery 
{
    public Guid FilmId { get; private set; }

    public GetFilmByIdQuery(Guid filmId)
    {
        FilmId = filmId;
    }
}