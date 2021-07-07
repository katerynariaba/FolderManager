using FolderManager.Domain.Models;
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
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Folder folder)
        {
            folder.Id = Guid.NewGuid().ToString();
            folder.Path = folder.Parent == null ? folder.Id :
                $"{folder.Parent.Path}/{folder.Id}";

            await _context.Folders.AddAsync(folder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FolderEditModel newFolder)
        {
            var folder = _context.Folders.Find(newFolder.Id);
            folder.Name = newFolder.Name;
            
            await _context.SaveChangesAsync();
        }

        public async Task MoveAsync(string id, string newParentId)
        {
            var folder = _context.Folders.Find(id);
            var newParent = _context.Folders.Find(newParentId);
            var children = _context.Folders.Where(r => r.Path.Contains(folder.Id) && r.Id != folder.Id);

            folder.Parent = newParent;
            folder.Path = newParent.Path + "/" + folder.Id;

            foreach (Folder child in children)
            {
                var oldPath = child.Path;
                var lastIndex = oldPath.IndexOf(folder.Id) + folder.Id.Length;
                child.Path = $"{folder.Path}{oldPath[lastIndex..]}";
            }

            await _context.SaveChangesAsync();
        }
    }
}
