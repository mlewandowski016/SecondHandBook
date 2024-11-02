using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IBookOfferService
    {
        int Create(CreateBookOfferDto dto);
        IEnumerable<BookOfferDto> GetAll();
        IEnumerable<BookOfferDto> GetByTakerId(int takerId);
        BookOfferDto GetById(int id);
        void Reserve(int bookOfferId, int takerId);
        void Collect(int bookOfferId, int takerId);
        
    }
    public class BookOfferService : IBookOfferService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public BookOfferService(SecondHandBookDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public IEnumerable<BookOfferDto> GetAll()
        {
            var bookOffers = _context.BookOffers.ToList();

            var results = _mapper.Map<List<BookOfferDto>>(bookOffers);

            /*var query = @"
        SELECT bo.BookId, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN, bo.GiverId, u.Name, u.Lastname
        FROM BookOffer bo
        LEFT JOIN Book b ON bo.BookId = b.Id
        LEFT JOIN User u ON bo.GiverId = u.Id";*/

            return results;
        }
        public int Create(CreateBookOfferDto dto)
        {
            var bookOffer = _mapper.Map<BookOffer>(dto);

            bookOffer.GiverId = _userContextService.GetUserId;
            bookOffer.DateOfOffer = DateTime.UtcNow;
            bookOffer.IsReserved = false;
            bookOffer.IsTaken = false;

            _context.BookOffers.Add(bookOffer);
            _context.SaveChanges();

            return bookOffer.Id;
        }

        public BookOfferDto GetById(int id)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == id);

            if (bookOffer == null)
                throw new NotFoundException("Display not found");

            var result = _mapper.Map<BookOfferDto>(bookOffer);

            return result;
        }

        public IEnumerable<BookOfferDto> GetByTakerId(int takerId)
        {
            var bookOffer = _context.BookOffers.Where(x => x.TakerId == takerId);

            if (bookOffer == null) return null;

            var results = _mapper.Map<List<BookOfferDto>>(bookOffer);

            return results;
        }
        public void Reserve(int bookOfferId, int takerId)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if(bookOffer == null)
                throw new NotFoundException("Book offer not found");

            bookOffer.TakerId = takerId;
            bookOffer.IsReserved = true;

            _context.SaveChanges();
        }

        public void Collect(int bookOfferId, int takerId)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if (bookOffer.TakerId != takerId)
                throw new NotFoundException("This book is reserved by someone else");

            bookOffer.IsTaken = true;

            _context.SaveChanges();
        }
    }
}

