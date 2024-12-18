using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IMyBookService
    {
        IEnumerable<MyBookDto> GetAll();
        void Delete(int bookId);
    }
    public class MyBookService : IMyBookService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;

        public MyBookService(SecondHandBookDbContext context, IUserContextService userContextService, IMapper mapper)
        {
            _context = context;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public IEnumerable<MyBookDto> GetAll()
        {
            var userId = _userContextService.GetUserId;

            if (userId is null)
                throw new NotFoundException("User not found");

            var myBooks = _context.MyBooks
                .Include(r => r.Book)
                .Where(r => r.OwnerId == userId)
                .ToList();

            if (myBooks is null)
                throw new NotFoundException("Books not found in yout library");

            var results = _mapper.Map<List<MyBookDto>>(myBooks);

            return results;
        }

        public void Delete(int bookId)
        {
            var myBook = _context.MyBooks.FirstOrDefault(x => x.Id == bookId);

            if (myBook is null)
                throw new NotFoundException("Book not found in yout library");

            _context.MyBooks.Remove(myBook);
            _context.SaveChanges();
        }
    }
}
