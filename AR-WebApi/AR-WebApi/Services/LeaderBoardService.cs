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

        public async Task<List<LeaderBoardItem>> GetList()
        {
            return await _context.LeaderBoardItems.ToListAsync();
        }
    }
}
