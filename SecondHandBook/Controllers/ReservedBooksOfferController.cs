using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{
    [Route("api/reservedOffers")]
    [ApiController]
    public class ReservedBooksOfferController : ControllerBase
    {
        private readonly IReservedBooksOfferService _reservedBooksOfferService;

        public ReservedBooksOfferController(IReservedBooksOfferService reservedBooksOfferService)
        {
            _reservedBooksOfferService = reservedBooksOfferService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookOfferDto>> GetAllReserved()
        {
            var reservedBooksOffer = _reservedBooksOfferService.GetAllReserved();

            return Ok(reservedBooksOffer);
        }

        [HttpPut("{bookOfferId}/collect")]
        public ActionResult Collect(int bookOfferId)
        {
            _reservedBooksOfferService.Collect(bookOfferId);

            return Ok();
        }
        [HttpPut("{bookOfferId}/unreserve")]
        public ActionResult Unreserve([FromRoute] int bookOfferId)
        {
            _reservedBooksOfferService.Unreserve(bookOfferId);

            return Ok();
        }
    }
}
