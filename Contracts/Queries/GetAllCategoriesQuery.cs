using Domain;
using MediatR;

namespace Contracts.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryDetails>>
    {
    }
}
