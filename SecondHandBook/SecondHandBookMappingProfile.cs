using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Models;

namespace SecondHandBook
{
    public class SecondHandBookMappingProfile : Profile
    {
        public SecondHandBookMappingProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<CreateBookDto, Book>();

            CreateMap<Display, DisplayDto>();

            CreateMap<CreateDisplayDto, Display>()
                .ForMember(dest => dest.DisplayDate, opt => opt.Ignore());

        }
    }
}
