using System.ComponentModel.DataAnnotations;

namespace AR_WebApi.DTOs
{
    public class LeaderBoardItemDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Score { get; set; }
    }
}
