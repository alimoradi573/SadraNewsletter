using Sadra.Newsletter.Application.DTOs;

namespace Sadra.Newsletter.Application.Services
{
    public interface INewsletterService
    {
        Task<NewsLetterDTO> GetAsync(int id);
        Task<int> CreateAsync(NewsLetterDTO NewsLetter);
        Task<NewsLetterDTO> UpdateAsync(NewsLetterDTO NewsLetter);
        Task<int> DeleteAsync(int id);
    }
}
