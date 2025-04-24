using Microsoft.AspNetCore.SignalR;

namespace AR_WebApi.SignalR
{
    public class UpdateHub : Hub
    {
        private readonly ILogger<UpdateHub> _logger;
        public UpdateHub(ILogger<UpdateHub> logger)
        {
            _logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation($"Connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }
    }
}
