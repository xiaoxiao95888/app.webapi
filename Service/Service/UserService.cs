using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Library.Services;

namespace Service.Service
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DataContext dbContext)
            : base(dbContext)
        {
        }
        public User GetUser(Guid id)
        {
            return DbContext.Users.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<User> GetUsers()
        {
            return DbContext.Users.Where(n => !n.IsDeleted);
        }

        public void Insert(User user)
        {
            DbContext.Users.Add(user);
            Update();
        }
    }
}
