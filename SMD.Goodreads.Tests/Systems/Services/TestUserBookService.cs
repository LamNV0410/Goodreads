using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.UserBooks;
using SMD.Goodreads.Tests.MockData;

namespace SMD.Goodreads.Tests.Systems.Services
{
    public class TestUserBookService : IDisposable
    {
        private readonly GoodReadsDbcontext _context;
        public TestUserBookService()
        {
            var options = new DbContextOptionsBuilder<GoodReadsDbcontext>()
                .UseInMemoryDatabase("TestGoodReadsDBContext")
                .Options;
            _context = new GoodReadsDbcontext(options);
            _context.Database.EnsureCreated();

            var userBookEntities = UserBookMockData.GetUserBook();
            _context.UserBooks.AddRange(userBookEntities);
            _context.SaveChangesAsync();
        }

        [Fact]
        public async Task ADD_USER_BOOK()
        {
            var currentCount = _context.UserBooks.Count();
            var entity = UserBookMockData.NewUserBook();
            var service = new UserBooksService(_context);
            await service.Add(entity);
            await _context.SaveChangesAsync();

            var added = _context.UserBooks
                .FirstOrDefaultAsync(x => x.BookId == entity.BookId && x.UserId == entity.UserId);
            added.Should().NotBeNull();
        }

        [Fact]
        public async Task GET_BY_ID_ASYNC()
        {
            var currentUserId = 2;
            var bookIdRequest = 2;
            var service = new UserBooksService(_context);
            var result = await service.GetByIdAsync(currentUserId, bookIdRequest);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GET_USER_BOOK_COMPLETED_READING()
        {
            var currentUserTest = 2;
            
            var bookEntities = BookMockData.GetBooks();
            _context.Books.AddRange(bookEntities);
            await _context.SaveChangesAsync();

            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var service = new UserBooksService(_context);
            var result = await service.GetUserBooksAsync(currentUserTest, request);
            result.Count().Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GET_USER_BOOK_NOT_COMPLETED_READING()
        {
            var currentUserTest = 3;
            var bookEntities = BookMockData.GetBooks();
            _context.Books.AddRange(bookEntities);
            await _context.SaveChangesAsync();

            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var service = new UserBooksService(_context);
            var result = await service.GetUserBooksAsync(currentUserTest, request);
            result.Count().Should().Be(0);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
