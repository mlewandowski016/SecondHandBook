using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IAvailableBookOfferService
    {
        int Create(CreateBookOfferDto dto);
        PageResult<BookOfferDto> GetAll(SearchQuery searchQuery);
        BookOfferDto GetById(int bookOfferId);
        void Reserve(int bookOfferId);
        
    }
    public class AvailableBooksOfferService : IAvailableBookOfferService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public AvailableBooksOfferService(SecondHandBookDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
        }

        public PageResult<BookOfferDto> GetAll(SearchQuery searchQuery)
        {
            /*var query = @"
            SELECT bo.Id, bo.DateOfOffer, bo.BookId, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN, bo.GiverId, u.Name, u.Lastname
            FROM BookOffers bo
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            AND bo.TakerId IS NULL";*/

            //var offers = _context.Set<BookOfferDto>().FromSqlRaw(query).AsNoTracking().ToList();

            var nonRaw = _context
                .BookOffers
                .Include(r => r.Book)
                .Include(r => r.Giver)
                .Where(r => searchQuery.SearchPhrase == null
                || r.Book.Title.ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                || r.Book.Author.ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                //|| r.Book.Category.ToString().ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                || r.Book.ISBN.ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                && r.TakerId == null);

            var offers = nonRaw
                .Skip(searchQuery.PageSize * (searchQuery.PageNumber - 1))
                .Take(searchQuery.PageSize)
                .ToList();

            var totalCount = nonRaw.Count();

            var offersDto = _mapper.Map<List<BookOfferDto>>(offers);

            var result = new PageResult<BookOfferDto>(offersDto, totalCount, searchQuery.PageSize, searchQuery.PageNumber);

            return result;
        }
        public int Create(CreateBookOfferDto dto)
        {
            var bookOffer = _mapper.Map<BookOffer>(dto);

            bookOffer.GiverId = _userContextService.GetUserId;
            bookOffer.DateOfOffer = DateTime.UtcNow;
            bookOffer.TakerId = null;
            bookOffer.IsCollected = false;

            _context.BookOffers.Add(bookOffer);
            _context.SaveChanges();

            return bookOffer.Id;
        }

        public BookOfferDto GetById(int bookOfferId)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            var query = @"
            SELECT bo.Id, bo.DateOfOffer, bo.BookId, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN, bo.GiverId, u.Name, u.Lastname
            FROM BookOffers bo
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            WHERE bo.Id = @bookOfferId
            AND bo.TakerId IS NULL";

            var result = _context.Set<BookOfferDto>()
                .FromSqlRaw(query, new SqlParameter("@bookOfferId", bookOfferId))
                .AsNoTracking()
                .FirstOrDefault();

            if (result == null)
                throw new NotFoundException("Book offers not found.");

            return result;
        }
        public void Reserve(int bookOfferId)
        {
            var userId = _userContextService.GetUserId;

            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if(bookOffer == null)
                throw new NotFoundException("Book offer not found");

            if(bookOffer.TakerId != null)
                throw new BadRequestException("This book is already reserved");

            bookOffer.TakerId = userId;

            _context.SaveChanges();
        }
    }
}

