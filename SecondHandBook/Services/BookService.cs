using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IBookService
    {
        BookDto GetById(int id);
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
            if (Enum.TryParse(dto.Category, true, out BookCategory category))
            {
                book.Category = category;
            }
            else
            {
                throw new NotFoundException("Category not found");
            }
            book.PublishDate = dto.PublishDate;
            book.PagesCount = dto.PagesCount;

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
