using Microsoft.AspNetCore.Mvc;
using SMD.Goodreads.API.Models.Entities;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using SMD.Goodreads.API.Services.UserBooks;
using SMD.Goodreads.API.Services.Users;
using System.Linq;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly IUserBooksService _userBooksService;
        private readonly IUserService _userService;
        public UserBooksController(
            IBooksService booksService,
            IUserBooksService userBooksService,
            IUserService userService
            )
        {
            _booksService = booksService;
            _userBooksService = userBooksService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserBooks([FromQuery]UserBooksModelRequest request)
        {
            var user = _userService.CurrentUser;
            var userBooks = await _userBooksService.GetUserBooksAsync(user.Id, request);

            if(userBooks is null || !userBooks.Any())
            {
                return NoContent();
            }

            return Ok(userBooks);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserReadingBooks(int bookId)
        {
            var user = _userService.CurrentUser;
            var book = await _booksService.GetByIdAsync(bookId);
            if(book is null)
            {
                return BadRequest($"Can't Find book by Id: {bookId}");
            }

            var existUserBook = await _userBooksService.GetByIdAsync(user.Id, bookId);
            if(existUserBook is not null)
            {
                return BadRequest($"Id is exist in UserBook entity");
            }

            var userBook = new UserBook()
            {
                UserId = user.Id,
                BookId = bookId,
                IsCompleted = false,
            };
            await _userBooksService.Add(userBook);
            return CreatedAtAction(nameof(GetUserBookById), new { id = userBook.BookId }, userBook);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserBookById(int id)
        {
            var user = _userService.CurrentUser;
            UserBook book = await _userBooksService.GetByIdAsync(user.Id, id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
    }
}
