using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Entities;
using SMD.Goodreads.API.Models.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Services.UserBooks
{
    public class UserBooksService : IUserBooksService
    {
        private readonly GoodReadsDbcontext _context;
        public UserBooksService(GoodReadsDbcontext context)
        {
            _context = context;
        }
        public async Task Add(UserBook entity)
        {
            _context.UserBooks.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserBook> GetByIdAsync(int userId, int bookId)
        {
            return await _context.UserBooks
                .FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);
        }

        public async Task<IEnumerable<Book>> GetUserBooksAsync(int userId, [FromQuery] UserBooksModelRequest request)
        {
            var userBooks = _context.Books.Include(x => x.UserBooks)
                .Where(_ => _.UserBooks.Any(x =>x.UserId == userId)).AsNoTracking();

            if (request != null && request.IsCompleted != null)
            {
                userBooks = userBooks.Where(_ => _.UserBooks.Any(x => x.IsCompleted == request.IsCompleted));
            }
            return await userBooks.ToListAsync();
        }

    }
}
