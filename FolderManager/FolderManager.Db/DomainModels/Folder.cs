using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolderManager.Db.DomainModels
{
    public class Folder
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public IList<Folder> Childrens { get; set; }
    }
}
