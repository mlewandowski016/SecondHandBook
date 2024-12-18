using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IReservedBooksOfferService
    {
        IEnumerable<BookOfferDto> GetAllReserved();
        void Collect(int bookOfferId);
        void Unreserve(int bookOfferId);
    }
    public class ReservedBooksOfferService : IReservedBooksOfferService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public ReservedBooksOfferService(SecondHandBookDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public IEnumerable<BookOfferDto> GetAllReserved()
        {
            var userId = _userContextService.GetUserId;

            if (userId == null)
                throw new NotFoundException("User not found");

            var bookOffers = _context
                .BookOffers
                .Include(r => r.Book)
                .Include(r => r.Giver)
                .Include(r => r.Taker)
                .Include(r => r.Images)
                .Where(r => r.TakerId.Equals(userId) && (r.IsCollected.Equals(false) || r.IsCollected.Equals(null)))
                .ToList();

            if (bookOffers == null)
                throw new NotFoundException("Reservations not found");

            var results = _mapper.Map<List<BookOfferDto>>(bookOffers);

            return results;
        }

        public void Collect(int bookOfferId)
        {
            var userId = _userContextService.GetUserId;

            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if (bookOffer.TakerId != userId)
                throw new BadRequestException("This book is reserved by someone else");

            var myBook = new MyBook();
            myBook.BookId = bookOfferId;
            myBook.OwnerId = userId;
            myBook.AddedDate = DateTime.UtcNow;

            _context.MyBooks.Add(myBook);

            bookOffer.IsCollected = true;

            _context.SaveChanges();
        }

        public void Unreserve(int bookOfferId)
        {
            var userId = _userContextService.GetUserId;

            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if (bookOffer.TakerId != userId)
                throw new BadRequestException("This book is reserved by someone else");

            bookOffer.TakerId = null;

            _context.SaveChanges();
        }
    }
}

