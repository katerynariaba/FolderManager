using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolderManager.Api.Models
{
    public class FolderViewModel
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z'':'\s]{1,40}$")]
        public string Name { get; set; }
        public virtual FolderViewModel Parent { get; set; }
        public List<FolderViewModel> Children { get; set; }

        public FolderViewModel()
        {
            Children = new List<FolderViewModel>();
        }
    }
}
