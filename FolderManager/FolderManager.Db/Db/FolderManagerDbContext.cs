using FolderManager.Db.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace FolderManager.Db.Db
{
    public class FolderManagerDbContext : DbContext
    {
        public FolderManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Folder> Folders { get; set; }
    }
}
