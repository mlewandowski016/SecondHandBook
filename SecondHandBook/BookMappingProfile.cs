using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Models;

namespace SecondHandBook
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<CreateBookDto, Book>();
        }
    }
}
