using Domain;
using MediatR;

namespace Contracts.Queries
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDetails>>;
}
