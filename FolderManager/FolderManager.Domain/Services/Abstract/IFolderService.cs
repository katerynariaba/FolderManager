using FolderManager.Domain.Models;
using FolderManager.Db.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderManager.Domain.Services.Abstract
{
    public interface IFolderService
    {
        public Task<IList<Folder>> GetAllAsync();
        public Task<IList<Folder>> GetOrderAsync();
        public Task<IList<Folder>> GetOrderDescendingAsync();
        public Task DeleteAsync(string id);
        public Task AddAsync(Folder folder);
        public Task<Folder> GetByIdAsync(string id);
        public Task UpdateAsync(FolderEditModel folder);
        public Task MoveAsync(string id, string newParentId);
    }
}
