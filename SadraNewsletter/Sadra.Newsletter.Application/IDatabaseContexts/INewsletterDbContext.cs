using Microsoft.EntityFrameworkCore;
using Sadra.Newsletter.Domain.Entities;

namespace Sadra.Newsletter.Application.IDatabaseContexts
{
    public interface INewsletterDbContext
    {
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
