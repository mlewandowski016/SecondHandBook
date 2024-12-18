using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<MyBookDto>> GetAll()
        {
            var myBooks = _myBookService.GetAll();

            return Ok(myBooks);
        }

        [HttpDelete("{bookId}")]
        public ActionResult Delete(int bookId)
        {
            _myBookService.Delete(bookId);

            return NoContent();
        }
    }
}
