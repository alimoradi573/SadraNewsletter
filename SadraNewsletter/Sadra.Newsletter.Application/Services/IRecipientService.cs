using Sadra.Newsletter.Application.DTOs;
using System.Linq.Expressions;

namespace Sadra.Newsletter.Application.Services
{
    public interface IRecipientService
    {
        Task<RecipientDTO> GetAsync(int id);
        Task<List<RecipientDTO>> GetAllAsync(string email);
        
        Task<int> CreateAsync(SendToRecipientDto recipientDTO);
        Task<int> CreateRangeAsync(List<SendToRecipientDto> recipientDTOs);

    }
}
