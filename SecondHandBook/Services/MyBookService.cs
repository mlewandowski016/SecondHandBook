using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IMyBookService
    {
        MyBook AddBookToMyLibrary(int bookId, int userId);
        IEnumerable<MyBookDto> GetAll();
        MyBookDto GetByBookId(int bookId);
        void Delete(int bookId);
    }
    public class MyBookService : IMyBookService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IUserContextService _userContextService;

        public MyBookService(SecondHandBookDbContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public MyBook AddBookToMyLibrary(int bookId, int userId)
        {
            var myBook = new MyBook();
            myBook.BookId = bookId;
            myBook.OwnerId = userId;
            myBook.AddedDate = DateTime.Now.Date;
            _context.MyBooks.Add(myBook);

            return myBook;
        }

        public IEnumerable<MyBookDto> GetAll()
        {
            var userId = _userContextService.GetUserId;
            var query = @"
            SELECT mb.Id, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN
            FROM MyBooks mb
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            WHERE mb.OwnerId = @userId";

            var result = _context.Set<MyBookDto>()
                .FromSqlRaw(query, new SqlParameter("@userId", userId))
                .AsNoTracking()
                .ToList();

            if (result == null)
                throw new NotFoundException("Could not find any books in your library");

            return result;
        }

        public MyBookDto GetByBookId(int bookId)
        {
            var userId = _userContextService.GetUserId;
            var query = @"
            SELECT mb.Id, b.Title, b.Author, b.Category, b.PagesCount, b.PublishDate, b.ISBN
            FROM MyBooks mb
            LEFT JOIN Books b ON bo.BookId = b.Id
            LEFT JOIN Users u ON bo.GiverId = u.Id
            WHERE mb.OwnerId = @userId
            AND mb.BookId = @bookId";

            var result = _context.Set<MyBookDto>()
                .FromSqlRaw(query, new SqlParameter("@userId", userId), new SqlParameter("@bookId", bookId))
                .AsNoTracking()
                .FirstOrDefault();

            if (result == null)
                throw new NotFoundException("Could not find any books in your library");

            return result;
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
