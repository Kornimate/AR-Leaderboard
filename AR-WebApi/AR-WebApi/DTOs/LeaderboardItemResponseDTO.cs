using System.ComponentModel.DataAnnotations;

namespace AR_WebApi.DTOs
{
    public class LeaderBoardItemResponseDTO
    {
        public string Key { get; set; } =string.Empty;
        public int Rank {  get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
