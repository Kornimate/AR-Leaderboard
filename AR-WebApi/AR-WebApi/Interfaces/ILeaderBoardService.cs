using AR_WebApi.DTOs;
using AR_WebApi.Models;

namespace AR_WebApi.Interfaces
{
    public interface ILeaderBoardService
    {
        Task<List<LeaderBoardItemResponseDTO>> GetList();
        Task<bool> ClearList();
        Task<bool> AddItemToList(LeaderBoardItemDTO item);
    }
}
