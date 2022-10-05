using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMD.Goodreads.API.Controllers;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.Tests.MockData;

namespace SMD.Goodreads.Tests.Systems.Controllers
{
    public class TestBooksController
    {
        [Fact]
        public async Task SMD_TEST_GETBOOKBYNAME()
        {
            var bookService = new Mock<IBooksService>();
            BookModelRequest request = new BookModelRequest()
            {
                Name = "Coven"
            };

            bookService.Setup(x => x.GetBooksAsync(request)).ReturnsAsync(BookMockData.GetBooks());
            var sut = new BooksController(bookService.Object);
            var result = await sut.GetBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
    }
}
