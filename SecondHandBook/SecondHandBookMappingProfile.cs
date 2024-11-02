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

            CreateMap<BookOffer, BookOfferDto>();

            CreateMap<CreateBookOfferDto, BookOffer>()
                .ForMember(dest => dest.DateOfOffer, opt => opt.Ignore());
                

        }
    }
}
