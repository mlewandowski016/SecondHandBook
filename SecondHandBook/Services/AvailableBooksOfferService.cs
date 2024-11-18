using AutoMapper;
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
        void EditOffer(int bookOfferId, UpdateBookOfferDto dto);
        void DeleteImage(int imageId);
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
            var filtr = _context
                .BookOffers
                .Include(r => r.Book)
                .Include(r => r.Giver)
                .Include(r => r.Taker)
                .Include(r => r.Images)
                .Where(r => searchQuery.SearchPhrase == null
                || r.Book.Title.ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                || r.Book.Author.ToLower().Contains(searchQuery.SearchPhrase.ToLower())
                || r.Book.ISBN.Contains(searchQuery.SearchPhrase.ToLower()))
                .Where(r => r.Taker.Equals(null));

            var offers = filtr
                .Skip(searchQuery.PageSize * (searchQuery.PageNumber - 1))
                .Take(searchQuery.PageSize)
                .ToList();

            var totalCount = filtr.Count();

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

            bookOffer.Images = new List<BookOfferImage>();
            foreach (var file in dto.Images)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var image = new BookOfferImage
                        {
                            ImageData = ms.ToArray(),
                            ContentType = file.ContentType
                        };
                        bookOffer.Images.Add(image);
                    }
                }
            }

            _context.BookOffers.Add(bookOffer);
            _context.SaveChanges();

            return bookOffer.Id;
        }
        public BookOfferDto GetById(int bookOfferId)
        {
            var bookOffer = _context.BookOffers
                .Include(r => r.Book)
                .Include(r => r.Giver)
                .Include(r => r.Images)
                .FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            var result = _mapper.Map<BookOfferDto>(bookOffer);

            return result;
        }

        public void EditOffer(int bookOfferId, UpdateBookOfferDto dto)
        {
            var bookOffer = _context.BookOffers.FirstOrDefault(x => x.Id == bookOfferId);

            if (bookOffer == null)
                throw new NotFoundException("Book offer not found");

            bookOffer.OfferDescription = dto.OfferDescription;
            bookOffer.Images = new List<BookOfferImage>();
            if (dto.Images != null)
            {
                foreach (var file in dto.Images)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var image = new BookOfferImage
                            {
                                ImageData = ms.ToArray(),
                                ContentType = file.ContentType
                            };
                            bookOffer.Images.Add(image);
                        }
                    }
                }
            }

            _context.SaveChanges();
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

        public void DeleteImage(int imageId)
        {
            var image = _context.BookOfferImages.FirstOrDefault(x => x.Id == imageId);

            if (image is null)
                throw new NotFoundException("Image not found");

            _context.BookOfferImages.Remove(image);
            _context.SaveChanges();
        }
    }
}

