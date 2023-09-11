using Sadra.Newsletter.Application.DTOs;
using System.Linq.Expressions;

namespace Sadra.Newsletter.Application.Services
{
    public interface INewsletterService
    {
        Task<NewsLetterDTO> GetAsync(int id);
        Task<int> CreateAsync(NewsLetterDTO NewsLetter);
 
    }
}
