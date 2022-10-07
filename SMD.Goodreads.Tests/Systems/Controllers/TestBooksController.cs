using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMD.Goodreads.API.Controllers;
using SMD.Goodreads.API.Models.Entities;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.Tests.MockData;

namespace SMD.Goodreads.Tests.Systems.Controllers
{
    public class TestBooksController
    {
        [Fact]
        public async Task GetBooks_WithNameUnMatch_ShouldReturnNoContent()
        {
            var bookService = new Mock<IBooksService>();
            var request = new BookModelRequest()
            {
                Name = "No Content"
            };
            bookService.Setup(x => x.GetBooksAsync(request)).ReturnsAsync(BookMockData.GetEmptyBook<List<Book>>());
            var controller = new BooksController(bookService.Object);
            var result = await controller.GetBooks(request);
            result.GetType().Should().Be(typeof(NoContentResult));
        }

        [Fact]
        public async Task GetBooks_WithNameMatch_ShouldReturnOkObjectResult()
        {
            var bookService = new Mock<IBooksService>();
            var request = new BookModelRequest()
            {
                Name = "Coven"
            };
            bookService.Setup(x => x.GetBooksAsync(request)).ReturnsAsync(BookMockData.GetBooks());
            var controller = new BooksController(bookService.Object);
            var result = await controller.GetBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
    }
}
