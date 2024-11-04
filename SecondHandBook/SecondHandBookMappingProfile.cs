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

            CreateMap<BookOffer, BookOfferDto>()
                .ForMember(x => x.Title, y => y.MapFrom(z => z.Book.Title))
                .ForMember(x => x.Author, y => y.MapFrom(z => z.Book.Author))
                .ForMember(x => x.PagesCount, y => y.MapFrom(z => z.Book.PagesCount))
                .ForMember(x => x.PublishDate, y => y.MapFrom(z => z.Book.PublishDate))
                .ForMember(x => x.ISBN, y => y.MapFrom(z => z.Book.ISBN))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Giver.Name))
                .ForMember(x => x.Lastname, y => y.MapFrom(z => z.Giver.Lastname));

            CreateMap<CreateBookOfferDto, BookOffer>()
                .ForMember(dest => dest.DateOfOffer, opt => opt.Ignore());
        }
    }
}
