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
        public async Task GetUserBooks_WithUserUnCompletedReadingAnyBook_ShouldReturnNoContentResult()
        {
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();
            
            userService.Setup(x => x.CurrentUser).Returns(currentUser);
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetEmptyBook());

            var controller = new UserBooksController(null, userBookService.Object, userService.Object);
            
            var result = await controller.GetUserBooks(request);
            result.GetType().Should().Be(typeof(NoContentResult));
        }

        [Fact]
        public async Task GetUserBooks_WithUserCompletedReading_ShouldReturnOkObjectResult()
        {
            var currentUser = new User()
            {
                Id = 1,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };
            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();

            userService.Setup(x => x.CurrentUser).Returns(currentUser);
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetBooksWithcompletedReading());

            var controller = new UserBooksController(null, userBookService.Object, userService.Object);
            
            var result = await controller.GetUserBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
        }
        
        [Fact]
        public async Task GetUserBooks_WithUserUnCompletedReading_ShouldReturnOkObjectResult()
        {
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var request = new UserBooksModelRequest()
            {
                IsCompleted = false
            };

            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();

            userService.Setup(x => x.CurrentUser).Returns(currentUser);
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetBooksWithUncompletedReading());

            var controller = new UserBooksController(null, userBookService.Object, userService.Object);
            
            var result = await controller.GetUserBooks(request);
            result.GetType().Should().Be(typeof(OkObjectResult));
        }

        [Fact]
        public async Task AddUserReadingBooks_WithCurrentUserAndWrongBookIdRequest_ShouldReturnBadObjectRequestResult()
        {
            var wrongBookId = 100;
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            UserBooksModelRequest request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();
            var bookService = new Mock<IBooksService>();

            userService.Setup(x => x.LoadCurrentUser()).Returns(currentUser);
            bookService.Setup(x => x.GetByIdAsync(wrongBookId)).ReturnsAsync(BookMockData.GetEmptyBook<Book>());
            userBookService.Setup(x => x.GetUserBooksAsync(currentUser.Id, request))
                .ReturnsAsync(UserBookMockData.GetEmptyBook());

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);
            
            var result = await controller.AddUserReadingBooks(wrongBookId);
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task AddUserReadingBooks_WithCurrentUserHaveReadBook_ShouldReturnBadRequestObjectResult()
        {
            var existIdBook = 2;
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();
            var bookService = new Mock<IBooksService>();

            userService.Setup(x => x.CurrentUser).Returns(currentUser);
            bookService.Setup(x => x.GetByIdAsync(existIdBook)).ReturnsAsync(BookMockData.GetById(existIdBook));
            userBookService.Setup(x => x.GetByIdAsync(currentUser.Id, existIdBook))
                .ReturnsAsync(UserBookMockData.GetUserBook());

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);

            var result = await controller.AddUserReadingBooks(existIdBook);
            result.GetType().Should().Be(typeof(BadRequestObjectResult));
        }

        [Fact]
        public async Task AddUserReadingBooks_WithCurrentUserAndbookId_ShouldReturnCreated()
        {
            var bookId = 1;
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Lam",
                LastName = "Nguyen"
            };
            var request = new UserBooksModelRequest()
            {
                IsCompleted = true
            };

            var userService = new Mock<IUserService>();
            var userBookService = new Mock<IUserBooksService>();
            var bookService = new Mock<IBooksService>();
            
            userService.Setup(x => x.CurrentUser).Returns(currentUser);
            bookService.Setup(x => x.GetByIdAsync(bookId)).ReturnsAsync(BookMockData.GetById(bookId));
            userBookService.Setup(x => x.GetByIdAsync(currentUser.Id, bookId))
                .ReturnsAsync(UserBookMockData.GetEmptyUserBook());

            var controller = new UserBooksController(bookService.Object, userBookService.Object, userService.Object);

            var result = await controller.AddUserReadingBooks(bookId);
            result.GetType().Should().Be(typeof(CreatedAtActionResult));
        }
    }
}
