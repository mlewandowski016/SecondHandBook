using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Models;
using SecondHandBook.Services;
using System.Security.Claims;

namespace SecondHandBook.Controllers
{

    [Route("api/display")]
    [ApiController]
    public class AvailableBooksController : ControllerBase
    {
        private readonly IBookOfferService _displayService;

        public AvailableBooksController(IBookOfferService displayService)
        {
            _displayService = displayService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookOfferDto>> GetAll()
        {
            var displaysDto = _displayService.GetAll();

            return Ok(displaysDto);
        }

        [HttpGet("taker/{takerId}")]
        public ActionResult<IEnumerable<BookOfferDto>> GetByTakerId([FromRoute] int takerId)
        {
            var displaysDto = _displayService.GetByTakerId(takerId);

            return Ok(displaysDto);
        }

        [HttpGet("{id}")]
        public ActionResult<BookOfferDto> GetById([FromRoute] int id)
        {
            var result = _displayService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateDisplay([FromBody] CreateBookOfferDto dto)
        {
            var id = _displayService.Create(dto);

            return Created($"/api/display/{id}", null);
        }

        [HttpPut("{displayId}/reserve")]
        public ActionResult Reserve([FromRoute] int displayId, [FromBody] ReserveBookOfferDto reserveDisplayDto)
        {
            _displayService.Reserve(displayId, reserveDisplayDto.TakerId);

            return Ok();
        }

        [HttpPut("{displayId}/collect")]
        public ActionResult Collect([FromRoute] int displayId, [FromBody] ReserveBookOfferDto reserveDisplayDto)
        {
            _displayService.Collect(displayId, reserveDisplayDto.TakerId);

            return Ok();
        }
    }
    
}
