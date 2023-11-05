using CreateCategory_Task.Entities;
using CreateCategory_Task.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Test.Entities;

namespace CreateCategory_Task.Contexts
{
    public class Entity_Context : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7VK5UN3\\SQLEXPRESS02;Database=TestDb;Trusted_Connection=true;TrustServerCertificate=true;");

            //base.OnConfiguring(optionsBuilder);  
            //    optionsBuilder.UseSqlite("Data Source=D:\\Task\\Test\\TaskDb.db;");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DateTime date = DateTime.UtcNow;
            TimeZoneInfo azTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            DateTime resultDate = TimeZoneInfo.ConvertTime(date, azTimeZone);

            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = resultDate,
                    EntityState.Modified => data.Entity.UpdatedDate = resultDate,
                    _ => resultDate,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
