using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FolderManager.Db.DomainModels
{
    public class Folder
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        [ForeignKey("ParentId")]
        public virtual Folder Parent { get; set; }

        public virtual IList<Folder> Children { get; set; }

        public Folder()
        {
            Children = new List<Folder>();
        }
    }
}
