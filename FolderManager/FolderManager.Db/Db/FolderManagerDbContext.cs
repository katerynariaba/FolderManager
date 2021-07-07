using FolderManager.Db.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace FolderManager.Db.Db
{
    public class FolderManagerDbContext : DbContext
    {
        public FolderManagerDbContext(DbContextOptions options) : base(options)
        {
            Database?.Migrate();
        }

        public DbSet<Folder> Folders { get; set; }
    }
}
