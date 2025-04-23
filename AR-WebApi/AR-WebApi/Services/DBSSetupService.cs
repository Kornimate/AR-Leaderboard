using AR_WebApi.Models;

namespace AR_WebApi.Services
{
    public static class DBSSetupService
    {
        public static void SetupDb(LeaderBoardDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
