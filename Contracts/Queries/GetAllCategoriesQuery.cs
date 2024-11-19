using Domain;
using MediatR;

namespace Contracts.Queries
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDetails>>;
}
