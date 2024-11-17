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
                .ForMember(x => x.Category, y => y.MapFrom(z => z.Book.Category))
                .ForMember(x => x.PagesCount, y => y.MapFrom(z => z.Book.PageCount))
                .ForMember(x => x.PublishDate, y => y.MapFrom(z => z.Book.PublishDate))
                .ForMember(x => x.ISBN, y => y.MapFrom(z => z.Book.ISBN))
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Giver.Name))
                .ForMember(x => x.Lastname, y => y.MapFrom(z => z.Giver.Lastname))
                .ForMember(x => x.BookDescription, y => y.MapFrom(z => z.Book.Description))
                .ForMember(x => x.Images, y => y.MapFrom(z =>z.Images.Select(image => new ImageDataDto
                {
                    Id = image.Id,
                    ContentType = image.ContentType,
                    Data = Convert.ToBase64String(image.ImageData)
                })
                .ToList()));

            CreateMap<CreateBookOfferDto, BookOffer>()
                .ForMember(dest => dest.DateOfOffer, opt => opt.Ignore())
                .ForMember(dest => dest.Images, opt => opt.Ignore());

            CreateMap<User, UserDto>();
        }
    }
}
