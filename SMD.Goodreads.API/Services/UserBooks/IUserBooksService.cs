using SMD.Goodreads.API.Models;
using SMD.Goodreads.API.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.UserBooks
{
    public interface IUserBooksService
    {
        Task AddBookRead(UserBook entity);
        Task<UserBook> GetByIdAsync(int userId, int bookId);
        Task<IEnumerable<Book>> GetUserBooksAsync(int userId, UserBooksModelRequest request);
    }
}
