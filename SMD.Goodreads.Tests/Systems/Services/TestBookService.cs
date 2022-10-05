using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.Tests.MockData;

namespace SMD.Goodreads.Tests.Systems.Services
{
    public class TestBookService : IDisposable
    {
        protected readonly GoodReadsDbcontext _context;
        public TestBookService()
        {
            var options = new DbContextOptionsBuilder<GoodReadsDbcontext>()
                .UseInMemoryDatabase("TestGoodReadsDBContext")
                .Options;
            _context = new GoodReadsDbcontext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GET_BY_ID_ASYNC()
        {
            var bookId = 1;
            _context.Books.AddRange(BookMockData.GetBooks());
            await _context.SaveChangesAsync();
            var bookService = new BooksService(_context);
            var result = await bookService.GetByIdAsync(bookId);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GET_BY_NAME_ASYNC()
        {
            var request = new BookModelRequest()
            {
                Name = "Coven"
            };
            _context.Books.AddRange(BookMockData.GetBooks());
            await _context.SaveChangesAsync();
            var bookService = new BooksService(_context);
            var result = await bookService.GetBooksAsync(request);
            result.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
