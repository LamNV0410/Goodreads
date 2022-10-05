using Microsoft.AspNetCore.Mvc;
using SMD.Goodreads.API.Models.Requests;
using SMD.Goodreads.API.Services.Books;
using System.Linq;
using System.Threading.Tasks;

namespace SMD.Goodreads.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<ActionResult> GetBooks([FromQuery]BookModelRequest request)
        {
            var result = await _booksService.GetBooksAsync(request);
            if (result is null || !result.Any())
            {
                return NoContent();
            }
            return Ok(result);
        }

    }
}
