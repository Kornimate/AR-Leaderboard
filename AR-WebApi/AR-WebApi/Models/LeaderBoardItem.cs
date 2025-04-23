using System.ComponentModel.DataAnnotations;

namespace AR_WebApi.Models
{
    public class LeaderBoardItem
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime RecordedTime { get; set; }
    }
}
