using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IReservedBooksOfferService
    {
        IEnumerable<BookOfferDto> GetAllReserved();
        BookOfferDto GetReservedById(int bookOfferId);
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
            var query = @"
            SELECT bo.Id, bo.DateOfOffer, bo.BookId, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN, bo.GiverId, u.Name, u.Lastname
            FROM BookOffers bo
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            WHERE bo.TakerId = @userId
            AND bo.IsCollected = 0";

            var results = _context.Set<BookOfferDto>()
                .FromSqlRaw(query, new SqlParameter("@userId", userId))
                .AsNoTracking()
                .ToList();

            if (results == null)
                throw new NotFoundException("Could not find reserved books.");

            return results;
        }

        public BookOfferDto GetReservedById(int bookOfferId)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            var userId = _userContextService.GetUserId;
            
            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if (bookOffer.TakerId != userId)
                throw new NotFoundException("This book is reserved by someone else");

            if (bookOffer.IsCollected == true)
                throw new NotFoundException("This offer is expired");



            var query = @"
            SELECT bo.Id, bo.DateOfOffer, bo.BookId, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN, bo.GiverId, u.Name, u.Lastname
            FROM BookOffers bo
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            WHERE bo.Id = @bookOfferId
            AND bo.TakerId = @userId
            AND bo.IsCollected = 0";

            var result = _context.Set<BookOfferDto>()
                .FromSqlRaw(query, new SqlParameter("@userId", userId), new SqlParameter("@bookOfferId", bookOfferId))
                .AsNoTracking()
                .FirstOrDefault();

            if (result == null)
                throw new NotFoundException("Could not find reserved book.");

            return result;
        }

        public void Collect(int bookOfferId)
        {
            var userId = _userContextService.GetUserId;

            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if (bookOffer.TakerId != userId)
                throw new BadRequestException("This book is reserved by someone else");

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

