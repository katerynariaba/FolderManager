using FolderManager.Db.Db;
using FolderManager.Db.DomainModels;
using FolderManager.Domain.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Folder> GetByIdAsync(string id)
        {
            return await _context.Folders.FindAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var folder = await _context.Folders
                .Include(r => r.Children)
                .FirstOrDefaultAsync(r => r.Id == id);

            if(folder.Children.Count > 0)
            {
                await DeleteChildrenAsync(folder.Id);
            }
            
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChildrenAsync(string id)
        {
            var children = await _context.Folders
                .Where(r => r.Parent.Id == id)
                .Include(r => r.Children)
                .ToListAsync();
            
            foreach (var child in children)
            {
                await DeleteChildrenAsync(child.Id);
                _context.Folders.Remove(child);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Folder folder)
        {
            var dbFolder = new Folder
            {
                Id = Guid.NewGuid().ToString(),
                Name = folder.Name,
                Path = folder.Parent.Path + "/" + folder.Id,
                Parent = folder.Parent

            };

            await _context.Folders.AddAsync(dbFolder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Folder folder)
        {
             _context.Folders.Update(folder);
            await _context.SaveChangesAsync();
        }
    }
}
