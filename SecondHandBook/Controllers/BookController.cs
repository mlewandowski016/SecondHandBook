using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public ActionResult CreateBook([FromBody] CreateBookDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _bookService.Create(dto);

            return Created($"/api/book/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetAll()
        {
            var booksDto = _bookService.GetAll();

            return Ok(booksDto);
        }

        [HttpGet("{id}")]
        public ActionResult<BookDto> GetById([FromRoute] int id)
        {
            var book = _bookService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateBookDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _bookService.Update(id, dto);
            if(!isUpdated)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _bookService.Delete(id);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
