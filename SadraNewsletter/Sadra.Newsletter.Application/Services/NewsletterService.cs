
using AutoMapper;
using Sadra.Newsletter.Application.DTOs;
using Sadra.Newsletter.Application.IDatabaseContexts;
using Sadra.Newsletter.Domain.Entities;

namespace Sadra.Newsletter.Application.Services
{
    public class NewsletterService : INewsletterService
    {
        private readonly INewsletterDbContext _newsletterDbContext;
        private readonly IMapper _mapper;

        public NewsletterService(IMapper mapper, INewsletterDbContext newsletterDbContext)
        {
            _mapper = mapper;
            _newsletterDbContext = newsletterDbContext;
        }
        public async Task<int> CreateAsync(NewsLetterDTO dto)
        {
            _newsletterDbContext.NewsLetters.Add(_mapper.Map<NewsLetter>(dto));
            return await _newsletterDbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var result = await _newsletterDbContext.NewsLetters.FindAsync(id);
            if (result != null)
            {
                _newsletterDbContext.NewsLetters.Remove(result);
                return await _newsletterDbContext.SaveChangesAsync();
            }
            return await _newsletterDbContext.SaveChangesAsync();
        }

        public async Task<NewsLetterDTO> GetAsync(int id)
        {
            var result = _newsletterDbContext.NewsLetters.Find(id);
            var res = _mapper.Map<NewsLetterDTO>(result);
            return await Task.FromResult(res);
        }

        public async Task<NewsLetterDTO> UpdateAsync(NewsLetterDTO item)
        {
            var result = _newsletterDbContext.NewsLetters.Find(item.Id);
            if (result != null)
            {
                _mapper.Map(item, result);
                _newsletterDbContext.NewsLetters.Update(result);
                _newsletterDbContext.SaveChanges();
            }
            var res = _mapper.Map<NewsLetterDTO>(result);
            return await Task.FromResult(res);
        }





    }
}
