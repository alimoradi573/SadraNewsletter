
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sadra.Newsletter.Application.DTOs;
using Sadra.Newsletter.Application.IDatabaseContexts;
using Sadra.Newsletter.Domain.Entities;

namespace Sadra.Newsletter.Application.Services
{
    public class RecipientService : IRecipientService
    {
        private readonly INewsletterDbContext _newsletterDbContext;
        private readonly IMapper _mapper;

        public RecipientService(IMapper mapper, INewsletterDbContext newsletterDbContext)
        {
            _mapper = mapper;
            _newsletterDbContext = newsletterDbContext;
        }
        public async Task<int> CreateAsync(SendToRecipientDto dto)
        {
            _newsletterDbContext.Recipients.Add(_mapper.Map<Recipient>(dto));
            return await _newsletterDbContext.SaveChangesAsync();
        }

        public async Task<int> CreateRangeAsync(List<SendToRecipientDto> recipientDTOs)
        {
            _newsletterDbContext.Recipients.AddRange(_mapper.Map<List<Recipient>>(recipientDTOs));
            return await _newsletterDbContext.SaveChangesAsync();
        }

        public async  Task<List<RecipientDTO>> GetAllAsync(string email)
        {
            var result = _newsletterDbContext.Recipients.Where(c => c.Email == email);
            var res = _mapper.Map<List<RecipientDTO>>(result);
            await UpdateLastViewed(email);
            return await Task.FromResult(res);

        }

        private async Task UpdateLastViewed(string email)
        {
            _newsletterDbContext.Recipients.Where(c => c.Email == email && c.GiveDate==null)
                        .ExecuteUpdate(setters =>  
                            setters .SetProperty(b => b.GiveDate, DateTime.UtcNow)
                        );

            await _newsletterDbContext.SaveChangesAsync();
        }

        public async Task<RecipientDTO> GetAsync(int id)
        {
            var result = _newsletterDbContext.Recipients.Find(id);
            if (result is not null)
            {
                result.LastViewed = DateTime.UtcNow;
                await _newsletterDbContext.SaveChangesAsync();
            }

            var res = _mapper.Map<RecipientDTO>(result);
            return await Task.FromResult(res);
        }

    }
}
