using CQRS.Application.Dtos;
using CQRS.Application.Queries;

namespace CQRS.Application.Interfaces;

public interface IGetFilmsQueryHandler
{
    Task<FilmsDto> Handle(GetFilmsQuery request, CancellationToken cancellationToken);
}