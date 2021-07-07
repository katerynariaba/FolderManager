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

        public async Task<IList<Folder>> GetOrderAsync()
        {
            return await _context.Folders.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<IList<Folder>> GetOrderDescendingAsync()
        {
            return await _context.Folders.OrderByDescending(r => r.Name).ToListAsync();
        }

        public async Task<Folder> GetByIdAsync(string id)
        {
            return await _context.Folders
                .Include(r => r.Children)
                .Include(r => r.Parent)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task DeleteAsync(string id)
        {
            var foldersToRemove = _context.Folders.Where(r => r.Path.Contains(id));
            _context.RemoveRange(foldersToRemove);
           
            var folder = await _context.Folders
                .Include(r => r.Children)
                .FirstOrDefaultAsync(r => r.Id == id);

             await DeleteChildrenAsync(folder.Children);
            
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteChildrenAsync(IList<Folder> children)
        {
           foreach(var child in children)
            {
                var innerChildren = await _context.Folders
                .Where(r => r.Parent.Id == child.Id)
                .Include(r => r.Children)
                .ToListAsync();

                await DeleteChildrenAsync(innerChildren);

                _context.Folders.Remove(child);
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
