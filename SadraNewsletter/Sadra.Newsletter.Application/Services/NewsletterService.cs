
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sadra.Newsletter.Application.DTOs;
using Sadra.Newsletter.Application.IDatabaseContexts;
using Sadra.Newsletter.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

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

        public async Task<NewsLetterDTO> GetAsync(int id)
        {
            var result = _newsletterDbContext.NewsLetters.Find(id);
            var res = _mapper.Map<NewsLetterDTO>(result);
            return await Task.FromResult(res);
        }
 
    }
}
