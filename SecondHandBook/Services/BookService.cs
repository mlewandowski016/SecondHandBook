using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IBookService
    {
        BookDto GetById(int id);
        IEnumerable<BookDto> GetAll();
        int Create(CreateBookDto dto);
        bool Update(int id, UpdateBookDto dto);
        bool Delete(int id);
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

            if(book == null) return null;

            var result = _mapper.Map<BookDto>(book);

            return result;
        }

        public IEnumerable<BookDto> GetAll()
        {
            var books = _context.Books.ToList();

            var results = _mapper.Map<List<BookDto>>(books);

            return results;
        }

        public int Create(CreateBookDto dto)
        {
            var book = _mapper.Map<Book>(dto);

            _context.Books.Add(book);
            _context.SaveChanges();

            return book.Id;
        }

        public bool Update(int id, UpdateBookDto dto)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book is null) return false;

            book.Title = dto.Title;
            book.Author = dto.Author;
            if (Enum.TryParse(dto.Category, true, out BookCategory category))
            {
                book.Category = category;
            }
            else
            {
                return false;
            }
            book.PublishDate = dto.PublishDate;
            book.PagesCount = dto.PagesCount;

            _context.SaveChanges();

            return true;
        }
        public bool Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if(book is null) return false;

            _context.Books.Remove(book);
            _context.SaveChanges();

            return true;
        }
    }
}
