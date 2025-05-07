using System.ComponentModel.DataAnnotations;

namespace AR_WebApi.Models
{
    public class RenameItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string OldName { get; set; } = string.Empty;

        [Required]
        public string NewName { get; set; } = string.Empty;
    }
}
