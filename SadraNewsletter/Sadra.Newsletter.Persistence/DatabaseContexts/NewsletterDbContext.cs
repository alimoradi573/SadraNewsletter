using Microsoft.EntityFrameworkCore;
using Sadra.Newsletter.Application.IDatabaseContexts;
using Sadra.Newsletter.Domain.Entities;
using System.Reflection;

namespace Sadra.Newsletter.Persistence.DatabaseContexts
{
    public class NewsletterDbContext : DbContext, INewsletterDbContext
    {


        public NewsletterDbContext(DbContextOptions<NewsletterDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType
                    .GetCustomAttributes(
                    typeof(Domain.Attributes.AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("CreateAt");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModifiedAt");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemovedAt");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");
                }
            }


            //modelBuilder.ApplyConfiguration(new BrandEntityConfig());
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            
 
        }

 
        public DbSet<NewsLetter> NewsLetters {get;set;}
        public DbSet<Recipient> Recipients { get; set; }

        public override int SaveChanges()
        {
            var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
              c.State == EntityState.Added ||
              c.State == EntityState.Deleted).ToList();
            foreach (var item in candidas)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var inserted = entityType.FindProperty("CreateAt");
                var modified = entityType.FindProperty("LastModifiedAt");
                var deleted = entityType.FindProperty("RemovedAt");
                var isRemoved = entityType.FindProperty("IsRemoved");
                if (item.State == EntityState.Added && inserted is not null)
                {
                    item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Modified && modified is not null)
                {
                    item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
                {
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
              c.State == EntityState.Added ||
              c.State == EntityState.Deleted).ToList();
            foreach (var item in candidas)
            {
                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var inserted = entityType.FindProperty("CreateAt");
                var modified = entityType.FindProperty("LastModifiedAt");
                var deleted = entityType.FindProperty("RemovedAt");
                var isRemoved = entityType.FindProperty("IsRemoved");
                if (item.State == EntityState.Added && inserted is not null)
                {
                    item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Modified && modified is not null)
                {
                    item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                }
                if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
                {
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

            return await base.SaveChangesAsync();
        }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
