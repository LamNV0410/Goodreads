using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMD.Goodreads.API.Controllers;
using SMD.Goodreads.API.Models;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.API.Services.UserBooks;
using SMD.Goodreads.API.Services.Users;
using SMD.Goodreads.Tests.MockData;

namespace SMD.Goodreads.Tests.Systems.Controllers
{
    public class TestUserBooksController
    {
        [Fact]
        public async Task SMD_TEST_GET_BOOKS_COMPLETED_READING()
        {
            User currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.LoadCurrentUser()).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            UserBooksModelRequest request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var sut = new UserBooksController(null, userBookService.Object, userService.Object);
            var result = await sut.GetUserBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task SMD_TEST_GET_BOOKS_NOT_COMPLETED_READING()
        {
            User currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.LoadCurrentUser()).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            UserBooksModelRequest request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var sut = new UserBooksController(null, userBookService.Object, userService.Object);
            var result = await sut.GetUserBooks(request);
            result.GetType().Should().Be(typeof(NoContentResult));
        }

        [Fact]
        public async Task SMD_TEST_MARK_BOOK_HAVE_READ_BUT_NO_HAVE_ID_REQUEST_BOOK()
        {
            var wrongBookId = 100;
            User currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.LoadCurrentUser()).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            UserBooksModelRequest request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var bookService = new Mock<IBooksService>();
            bookService.Setup(x => x.GetByIdAsync(wrongBookId)).ReturnsAsync(BookMockData.GetEmptyBook());

            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var sut = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);
            var result = await sut.AddUserReadingBooks(wrongBookId);
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task SMD_TEST_MARK_BOOK_HAVE_READ()
        {
            User currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.LoadCurrentUser()).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            UserBooksModelRequest request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));
            var sut = new UserBooksController(null, userBookService.Object, userService.Object);
            var result = await sut.GetUserBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);
        }
    }
}
