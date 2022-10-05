using SMD.Goodreads.API.Models.Entities;
using SMD.Goodreads.API.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.Books
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync(BookModelRequest request);
        Task<Book> GetByIdAsync(int id);
    }
}
