using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public interface IUserService : IDisposable
    {
        User GetUser(Guid id);
        IQueryable<User> GetUsers();
        void Insert(User user);
        void Update();
    }
}
