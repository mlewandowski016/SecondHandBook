using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{

    [Route("api/display")]
    [ApiController]
    public class DisplayController : ControllerBase
    {
        private readonly IDisplayService _displayService;

        public DisplayController(IDisplayService displayService)
        {
            _displayService = displayService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DisplayDto>> GetAll()
        {
            var displaysDto = _displayService.GetAll();

            return Ok(displaysDto);
        }

        [HttpGet("taker/{takerId}")]
        public ActionResult<IEnumerable<DisplayDto>> GetByTakerId([FromRoute] int takerId)
        {
            var displaysDto = _displayService.GetByTakerId(takerId);

            return Ok(displaysDto);
        }

        [HttpGet("{id}")]
        public ActionResult<DisplayDto> GetById([FromRoute] int id)
        {
            var result = _displayService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateDisplay([FromBody] CreateDisplayDto dto)
        {
            var id = _displayService.Create(dto);

            return Created($"/api/display/{id}", null);
        }

        [HttpPut("{displayId}/reserve")]
        public ActionResult Reserve([FromRoute] int displayId, [FromBody] ReserveDisplayDto reserveDisplayDto)
        {
            _displayService.Reserve(displayId, reserveDisplayDto.TakerId);

            return Ok();
        }

        [HttpPut("{displayId}/collect")]
        public ActionResult Collect([FromRoute] int displayId, [FromBody] ReserveDisplayDto reserveDisplayDto)
        {
            _displayService.Take(displayId, reserveDisplayDto.TakerId);

            return Ok();
        }
    }
    
}
