using Domain;

namespace Data.EF.Repositories;

public interface IUsersRepository
{
    Task<IEnumerable<UserDetails>> GetAllUsersAsync();
}
