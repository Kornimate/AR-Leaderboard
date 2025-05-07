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
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var renameItem = await _context.RenameItems.FirstOrDefaultAsync(x => x.OldName == item.Name);

                await _context.LeaderBoardItems.AddAsync(new()
                {
                    Id = Guid.NewGuid(),
                    Name = renameItem?.NewName ?? item.Name,
                    Score = item.Score,
                    RecordedTime = DateTime.UtcNow,
                });

                await transaction.CommitAsync();

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

        public async Task<bool> RenameTeamOrPlaceOrder(RenameItemDTO newItem)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var item = await _context.LeaderBoardItems.FirstOrDefaultAsync(x => x.Name == newItem.OldName);

                if (item != null)
                {
                    item.Name = newItem.NewName;
                    await transaction.CommitAsync();
                    await _context.SaveChangesAsync();
                    await _hub.Clients.All.SendAsync(UPDATE_MESSAGE);
                    return true;
                }

                await _context.RenameItems.AddAsync(new RenameItem()
                {
                    Id = Guid.NewGuid(),
                    OldName = newItem.OldName,
                    NewName = newItem.NewName
                });

                await transaction.CommitAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
