using Contracts.Queries;
using Data.EF.Repositories;
using Domain;
using MediatR;

namespace Services.MediatR.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDetails>>
    {
        private readonly IUsersRepository _usersRepository;

        public GetAllUsersHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<IEnumerable<UserDetails>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetAllUsersAsync(cancellationToken);
        }
    }
}
