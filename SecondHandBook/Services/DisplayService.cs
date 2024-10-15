using AutoMapper;
using SecondHandBook.Entities;
using SecondHandBook.Exceptions;
using SecondHandBook.Models;

namespace SecondHandBook.Services
{
    public interface IDisplayService
    {
        int Create(CreateDisplayDto dto);
        IEnumerable<DisplayDto> GetAll();
        IEnumerable<DisplayDto> GetByTakerId(int takerId);
        DisplayDto GetById(int id);
        void Reserve(int displayId, int takerId);
        void Take(int displayId, int takerId);
        
    }
    public class DisplayService : IDisplayService
    {
        private readonly SecondHandBookDbContext _context;
        private readonly IMapper _mapper;

        public DisplayService(SecondHandBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<DisplayDto> GetAll()
        {
            var displays = _context.Displays.ToList();

            var results = _mapper.Map<List<DisplayDto>>(displays);

            return results;
        }
        public int Create(CreateDisplayDto dto)
        {
            var display = _mapper.Map<Display>(dto);

            display.DisplayDate = DateTime.UtcNow;
            display.IsReserved = false;
            display.IsTaken = false;

            _context.Displays.Add(display);
            _context.SaveChanges();

            return display.Id;
        }

        public DisplayDto GetById(int id)
        {
            var display = _context.Displays.FirstOrDefault(x => x.Id == id);

            if (display == null)
                throw new NotFoundException("Display not found");

            var result = _mapper.Map<DisplayDto>(display);

            return result;
        }

        public IEnumerable<DisplayDto> GetByTakerId(int takerId)
        {
            var display = _context.Displays.Where(x => x.TakerId == takerId);

            if (display == null) return null;

            var results = _mapper.Map<List<DisplayDto>>(display);

            return results;
        }
        public void Reserve(int displayId, int takerId)
        {
            var display = _context.Displays.FirstOrDefault(x => x.Id == displayId);

            if(display == null)
                throw new NotFoundException("Display not found");

            display.TakerId = takerId;
            display.IsReserved = true;

            _context.SaveChanges();
        }

        public void Take(int displayId, int takerId)
        {
            var display = _context.Displays.FirstOrDefault(x => x.Id == displayId);

            if (display == null)
                throw new NotFoundException("Display not found");

            if (display.TakerId != takerId)
                throw new NotFoundException("This book is reserved by someone else");

            display.IsTaken = true;

            _context.SaveChanges();
        }
    }
}

