using Domain;
using MediatR;

namespace Contracts.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDetails>>
    {
    }
}
