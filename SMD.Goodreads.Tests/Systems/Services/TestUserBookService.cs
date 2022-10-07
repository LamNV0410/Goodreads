using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.UserBooks;
using SMD.Goodreads.Tests.MockData;
using SMD.Goodreads.Tests.MockDataContext;

namespace SMD.Goodreads.Tests.Systems.Services
{
    public class TestUserBookService : IDisposable
    {
        private readonly GoodReadsDbcontext _context;

        public TestUserBookService()
        {
            var options = MockDataContextOptions.GetContextOptions<GoodReadsDbcontext>("TestUserBookServiceDb");
            _context = new GoodReadsDbcontext(options);
            _context.Database.EnsureCreated();

            var userBookEntities = UserBookMockData.GetUserBook();
            _context.UserBooks.AddRange(userBookEntities);
            _context.SaveChangesAsync();
        }

        [Fact]
        public async Task AddUserBook_WithEntity_ReturnNotNull()
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
        public async Task GetByIdAsync_WithCurrentUserAndBookId_ShouldReturnNotNull()
        {
            var currentUserId = 2;
            var bookIdRequest = 2;
            var service = new UserBooksService(_context);
            var result = await service.GetByIdAsync(currentUserId, bookIdRequest);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_WithCurrentUserAndWrongBookId_ShouldReturnNull()
        {
            var currentUserId = 2;
            var wrongBookId = 200;
            var service = new UserBooksService(_context);
            var result = await service.GetByIdAsync(currentUserId, wrongBookId);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetUserBooksAsync_WithUserCompletedReading_ShouldReturnGreaterThan0()
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
        public async Task GetUserBooksAsync_WithUserNoCompletedReading_ShouldReturnEmpty()
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
            GC.SuppressFinalize(this);
        }
    }
}
