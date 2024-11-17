using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IBookService
    {
        BookDto GetById(int id);
        SearchListResult<BookDto> GetBookList(string searchPhrase);
        IEnumerable<BookDto> GetAll();
        int Create(CreateBookDto dto);
        void Update(int id, UpdateBookDto dto);
        void Delete(int id);
    }
    public class BookService : IBookService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IMapper _mapper;

        public BookService(SecondHandBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDto GetById(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book == null)
                throw new NotFoundException("Book not found"); 


            var result = _mapper.Map<BookDto>(book);

            return result;
        }

        public IEnumerable<BookDto> GetAll()
        {
            var books = _context.Books.ToList();

            var results = _mapper.Map<List<BookDto>>(books);

            return results;
        }

        public SearchListResult<BookDto> GetBookList(string searchPhrase)
        {
            var filtr = _context
                .Books
                .Where(r => r.Title.ToLower().Contains(searchPhrase.ToLower())
                || r.Author.ToLower().Contains(searchPhrase.ToLower())
                || r.ISBN.Contains(searchPhrase.ToLower()))
                .ToList();

            var books = _mapper.Map<List<BookDto>>(filtr);

            var result = new SearchListResult<BookDto>(books);

            return result;
        }

        public int Create(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            _context.Books.Add(book);
            _context.SaveChanges();

            return book.Id;
        }

        public void Update(int id, UpdateBookDto dto)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book is null)
                throw new NotFoundException("Book not found");

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.Category = dto.Category;
            book.PublishDate = dto.PublishDate;
            book.PageCount = dto.PagesCount;

            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book is null)
                throw new NotFoundException("Book not found");

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
