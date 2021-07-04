using FolderManager.Db.Db;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FolderManager.Domain.Services.Concrete
{
    public class FolderService : IFolderService
    {
        private readonly FolderManagerDbContext _context;

        public FolderService(FolderManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Folder>> GetAllAsync()
        {
            return await _context.Folders.ToListAsync();
        }

        public async Task<IList<Folder>> GetRootAsync()
        {
            return await _context.Folders
                .Include(r => r.Parent)
                .Where(r => r.Id == r.Parent.Id)
                .ToListAsync();
        }

        public async Task<IList<Folder>> GetChildrenAsync(string id)
        {
            return await _context.Folders
                .Include(r => r.Parent)
                .Where(r => r.Parent.Id == id && r.Id != id)
                .ToListAsync();
        }
    }
}
