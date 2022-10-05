using Microsoft.AspNetCore.Mvc;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksRepository)
        {
            _booksService = booksRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks([FromQuery]BookModelRequest request)
        {
            return Ok(await _booksService.GetBooksAsync(request));
        }

    }
}
