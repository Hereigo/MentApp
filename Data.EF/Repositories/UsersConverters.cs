using Data.EF.Models;
using Domain;

namespace Data.EF.Repositories
{
    public static class UsersConverters
    {
        public static User FromDomain(this UserDetails user)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }
        public static UserDetails ToDomain(this User user)
        {
            return new UserDetails
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }
    }
}
