using AR_WebApi.DTOs;
using AR_WebApi.Interfaces;
using AR_WebApi.Models;
using AR_WebApi.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AR_WebApi.Services
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private static readonly string UPDATE_MESSAGE = "ReceiveUpdate";

        private readonly LeaderBoardDbContext _context;
        private readonly IHubContext<UpdateHub> _hub;
        public LeaderBoardService(LeaderBoardDbContext context, IHubContext<UpdateHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<bool> AddItemToList(LeaderBoardItemDTO item)
        {
            try
            {
                await _context.LeaderBoardItems.AddAsync(new()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Score = item.Score,
                    RecordedTime = DateTime.UtcNow,
                });

                await _context.SaveChangesAsync();

                await _hub.Clients.All.SendAsync(UPDATE_MESSAGE);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ClearList()
        {
            try
            {
                _context.LeaderBoardItems.RemoveRange(_context.LeaderBoardItems);

                await _context.SaveChangesAsync();

                await _hub.Clients.All.SendAsync(UPDATE_MESSAGE);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<LeaderBoardItemResponseDTO>> GetList()
        {
            var listOfItems = await _context.LeaderBoardItems
                                    .OrderByDescending(x => x.Score)
                                    .ThenByDescending(x => x.RecordedTime)
                                    .Select(x => new LeaderBoardItemResponseDTO()
                                    {
                                        Key = x.Id.ToString(),
                                        Name = x.Name,
                                        Score = x.Score,
                                    })
                                    .ToListAsync();

            int currentRank = 1;
            int sameRankCounter = 0;

            for (int i = 0; i < listOfItems.Count; i++)
            {
                if (i > 0 && listOfItems[i].Score < listOfItems[i - 1].Score)
                {
                    listOfItems[i].Rank = (currentRank += sameRankCounter);
                    sameRankCounter = 1;
                }
                else
                {
                    listOfItems[i].Rank = currentRank;
                    ++sameRankCounter;
                }
            }

            return listOfItems;
        }
    }
}
