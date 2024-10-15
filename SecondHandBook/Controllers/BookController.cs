using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{
    [Route("api/book")]
    [ApiController]
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
            var result =  _bookService.GetById(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateBookDto dto, [FromRoute] int id)
        {
            _bookService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _bookService.Delete(id);

            return NoContent();
        }
    }
}
