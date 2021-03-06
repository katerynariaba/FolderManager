using System.Collections.Generic;

namespace FolderManager.Domain.Models
{
    public class FolderViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual FolderViewModel Parent { get; set; }
        public List<FolderViewModel> Children { get; set; }

        public FolderViewModel()
        {
            Children = new List<FolderViewModel>();
        }
    }
}
