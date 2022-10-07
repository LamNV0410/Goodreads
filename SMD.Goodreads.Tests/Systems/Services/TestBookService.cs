using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SMD.Goodreads.API.Context;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.Tests.MockData;
using SMD.Goodreads.Tests.MockDataContext;

namespace SMD.Goodreads.Tests.Systems.Services
{
    public class TestBookService : IDisposable
    {
        protected readonly GoodReadsDbcontext _context;

        public TestBookService()
        {
            var options = MockDataContextOptions.GetContextOptions<GoodReadsDbcontext>("TestBookServiceDb");
            _context = new GoodReadsDbcontext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetByIdAsync_WithHasId_ShouldReturnNotNull()
        {
            var bookId = 1;

            _context.Books.AddRange(BookMockData.GetBooks());
            await _context.SaveChangesAsync();

            var bookService = new BooksService(_context);

            var result = await bookService.GetByIdAsync(bookId);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByIdAsync_WithHasNoId_ShouldReturnNull()
        {
            var bookId = 100;
            _context.Books.AddRange(BookMockData.GetBooks());
            await _context.SaveChangesAsync();

            var bookService = new BooksService(_context);
            var result = await bookService.GetByIdAsync(bookId);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GetBookAsync_WithNameIsMatch_ShouldReturnGreaterThanOrEqualToOne()
        {
            var request = new BookModelRequest()
            {
                Name = "Coven"
            };
            _context.Books.AddRange(BookMockData.GetBooksWithUncompletedReading());
            await _context.SaveChangesAsync();

            var bookService = new BooksService(_context);
            var result = await bookService.GetBooksAsync(request);

            result.Count().Should().BeGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async Task GetBookAsync_WithNameIsEmpty_ShouldReturnAllBook()
        {
            var request = new BookModelRequest()
            {
                Name = ""
            };
            _context.Books.AddRange(BookMockData.GetBooks());
            await _context.SaveChangesAsync();

            var bookService = new BooksService(_context);

            var result = await bookService.GetBooksAsync(request);
            result.Count().Should().Be(BookMockData.GetBooks().Count);
        }

        [Fact]
        public async Task GetBookAsync_WithNameIsNotMatch_ShouldReturnEmpty()
        {
            var request = new BookModelRequest()
            {
                Name = "Name is not match"
            };
            _context.Books.AddRange(BookMockData.GetBooks());

            await _context.SaveChangesAsync();
            var bookService = new BooksService(_context);

            var result = await bookService.GetBooksAsync(request);
            result.Count().Should().Be(0);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
