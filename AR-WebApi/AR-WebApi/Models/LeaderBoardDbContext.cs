using Microsoft.EntityFrameworkCore;

namespace AR_WebApi.Models
{
    public class LeaderBoardDbContext(DbContextOptions<LeaderBoardDbContext> options) : DbContext(options)
    {
        public virtual DbSet<LeaderBoardItem> LeaderBoardItems { get; set; } = null!;
        public virtual DbSet<RenameItem> RenameItems { get; set; } = null!;
    }
}
