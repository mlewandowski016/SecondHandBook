using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Entities;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{
    [Route("api/myBooks")]
    [ApiController]
    public class MyBookController : ControllerBase
    {
        private readonly IMyBookService _myBookService;
        public MyBookController(IMyBookService myBookService)
        {
            _myBookService = myBookService;
        }

        [HttpPost]
        public ActionResult<MyBook> Create(int bookId, int userId)
        {
            var myBook = _myBookService.AddBookToMyLibrary(bookId, userId);

            return Created($"/api/book/{myBook.Id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<MyBookDto>> GetAll()
        {
            var myBooks = _myBookService.GetAll();

            return Ok(myBooks);
        }

        [HttpGet("{bookId}")]
        public ActionResult<MyBookDto> GetByBookId(int bookId)
        {
            var myBook = _myBookService.GetByBookId(bookId);

            return Ok(myBook);
        }

        [HttpDelete("{bookId}")]
        public ActionResult Delete(int bookId)
        {
            _myBookService.Delete(bookId);

            return NoContent();
        }
    }
}
