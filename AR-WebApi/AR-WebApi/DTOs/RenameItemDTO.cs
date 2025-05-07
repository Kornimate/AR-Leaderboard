using System.ComponentModel.DataAnnotations;

namespace AR_WebApi.DTOs
{
    public class RenameItemDTO
    {
        [Required]
        public string OldName { get; set; } = string.Empty;

        [Required]
        public string NewName { get; set; } = string.Empty;
    }
}
