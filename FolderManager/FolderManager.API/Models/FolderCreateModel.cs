using System.ComponentModel.DataAnnotations;

namespace FolderManager.Api.Models
{
    public class FolderCreateModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z'':'\s]{1,40}$")]
        public string Name { get; set; }
    }
}
