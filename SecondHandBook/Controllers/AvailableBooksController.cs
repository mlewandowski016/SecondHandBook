﻿using Microsoft.AspNetCore.Mvc;
using SecondHandBook.Models;
using SecondHandBook.Services;

namespace SecondHandBook.Controllers
{

    [Route("api/offers")]
    [ApiController]
    public class AvailableBooksController : ControllerBase
    {
        private readonly IAvailableBookOfferService _bookOfferService;

        public AvailableBooksController(IAvailableBookOfferService bookOfferService)
        {
            _bookOfferService = bookOfferService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookOfferDto>> GetAll([FromQuery] SearchQuery query)
        {
            var bookOfferDto = _bookOfferService.GetAll(query);

            return Ok(bookOfferDto);
        }

        [HttpGet("{id}")]
        public ActionResult<BookOfferDto> GetById([FromRoute] int id)
        {
            var result = _bookOfferService.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateBookOffer([FromBody] CreateBookOfferDto dto)
        {
            var id = _bookOfferService.Create(dto);

            return Created($"/api/offers/{id}", null);
        }

        [HttpPut("{bookOfferId}/reserve")]
        public ActionResult Reserve([FromRoute] int bookOfferId)
        {
            _bookOfferService.Reserve(bookOfferId);

            return Ok();
        }
    }
}
