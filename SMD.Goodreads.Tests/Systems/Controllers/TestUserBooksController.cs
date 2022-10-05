using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SMD.Goodreads.API.Controllers;
using SMD.Goodreads.API.Models;
using SMD.Goodreads.API.Models.Entities;
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
        public async Task NO_CONTENT_READING_TEST_GET_BOOKS()
        {
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CurrentUser).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var controller = new UserBooksController(null, userBookService.Object, userService.Object);
            var result = await controller.GetUserBooks(request);
            result.GetType().Should().Be(typeof(NoContentResult));
        }

        [Fact]
        public async Task COMPLETED_READING_TEST_GET_BOOKS()
        {
            var currentUser = new User()
            {
                Id = 1,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CurrentUser).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var controller = new UserBooksController(null, userBookService.Object, userService.Object);
            var result = await controller.GetUserBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task WRONG_BOOK_ID_ADD_USER_READING_BOOK()
        {
            var wrongBookId = 100;
            var currentUser = new User()
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
            bookService.Setup(x => x.GetByIdAsync(wrongBookId)).ReturnsAsync(BookMockData.GetEmptyBook<Book>());

            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetUserBooksCompletedReading(currentUser.Id));

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);
            var result = await controller.AddUserReadingBooks(wrongBookId);
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task EXIST_BOOK_ID_ADD_USER_READING_BOOK()
        {
            var existIdBook = 2;
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CurrentUser).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var bookService = new Mock<IBooksService>();
            bookService.Setup(x => x.GetByIdAsync(existIdBook)).ReturnsAsync(BookMockData.GetById(existIdBook));

            userBookService.Setup(x => x.GetByIdAsync(currentUser.Id, existIdBook))
                .ReturnsAsync(UserBookMockData.GetUserBook());

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);
            var result = await controller.AddUserReadingBooks(existIdBook);
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task ADD_USER_READING_BOOK()
        {
            var bookId = 1;
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var userService = new Mock<IUserService>();
            userService.Setup(x => x.CurrentUser).Returns(currentUser);

            var userBookService = new Mock<IUserBooksService>();
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var bookService = new Mock<IBooksService>();
            bookService.Setup(x => x.GetByIdAsync(bookId)).ReturnsAsync(BookMockData.GetById(bookId));

            userBookService.Setup(x => x.GetByIdAsync(currentUser.Id, bookId))
                .ReturnsAsync(UserBookMockData.GetEmptyUserBook());

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);
            var result = await controller.AddUserReadingBooks(bookId);
            result.GetType().Should().Be(typeof(CreatedAtActionResult));
        }
    }
}
