using AR_WebApi.Models;

namespace AR_WebApi.Interfaces
{
    public interface ILeaderBoardService
    {
        Task<List<LeaderBoardItem>> GetList();
        Task<bool> ClearList();
        Task<bool> AddItemToList(LeaderBoardItemDTO item);
    }
}
