using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Entities;
using SMD.Goodreads.API.Models.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.Books
{
    public class BooksService : IBooksService
    {
        private readonly GoodReadsDbcontext _context;

        public BooksService(GoodReadsDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(BookModelRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Name)) return await GetByName(request.Name);
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        private async Task<IEnumerable<Book>> GetByName(string name)
        {
            return await _context.Books.Where(x => x.Name.Contains(name)).AsNoTracking().ToListAsync();
        }
    }
}
