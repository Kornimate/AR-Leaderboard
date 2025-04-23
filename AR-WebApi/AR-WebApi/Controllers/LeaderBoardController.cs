using AR_WebApi.Controllers.ActionFilters;
using AR_WebApi.Interfaces;
using AR_WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AR_WebApi.Controllers
{
    [ApiController]
    [Route("api/leaderboard")]
    public class LeaderBoardController : ControllerBase
    {
        private const string HEADER_KEY = "X-Api-Key";

        private readonly ILeaderBoardService _service;
        private readonly IConfiguration _configuration;

        public LeaderBoardController(ILeaderBoardService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }


        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok(await _service.GetList());
        }

        [HttpPost("add")]
        [AuthorizationFilter(HEADER_KEY)]
        public async Task<IActionResult> AddNewItem(LeaderBoardItemDTO newItem)
        {
            var succcessfulOperation = await _service.AddItemToList(newItem);
            
            return succcessfulOperation 
                ? StatusCode(StatusCodes.Status201Created) 
                : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("clear")]
        [AuthorizationFilter(HEADER_KEY)]
        public async Task<IActionResult> ClearItems()
        {
            var succcessfulOperation = await _service.ClearList();

            return succcessfulOperation 
                ? StatusCode(StatusCodes.Status201Created)
                : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [NonAction]
        public string GetApiKey()
        {
            return _configuration[HEADER_KEY]!;
        }
    }
}
