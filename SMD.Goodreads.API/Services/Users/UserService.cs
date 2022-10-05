using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Entities;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.Users
{
    public class UserService : IUserService
    {
        private readonly GoodReadsDbcontext _context;
        public User CurrentUser { get; set; }
        public UserService(GoodReadsDbcontext context)
        {
            _context = context;
            CurrentUser = LoadCurrentUser();
        }
        public UserService(User user)
        {
            CurrentUser = user;
        }
        public User LoadCurrentUser()
        {
            return new User()
            {
                Id = 1,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
        }
        public Task<User> GetUserByIdAsync(int id)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
