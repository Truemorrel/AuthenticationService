using System.Collections.Generic;
using AuthenticationService.Model.DL;

namespace AuthenticationService.DAL.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}
