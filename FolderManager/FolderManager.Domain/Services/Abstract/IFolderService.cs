using FolderManager.Db.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderManager.Domain.Services.Abstract
{
    public interface IFolderService
    {
        public Task<IList<Folder>> GetAllAsync();
        public Task<IList<Folder>> GetRootAsync();
        public Task<IList<Folder>> GetChildrenAsync(string id);
    }
}
