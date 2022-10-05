using SMD.Goodreads.API.Models.Entities;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.Users
{
    public interface IUserService
    {
        User CurrentUser { get; set; }
        User LoadCurrentUser();
        Task<User> GetUserByIdAsync(int Id);
    }
}
