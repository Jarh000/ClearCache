using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace APIServer.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("clear")]
        public IActionResult GetVersion()
        {
            var redis = ConnectionMultiplexer.Connect("127.0.0.1:6385,password=v3xtVOYVdlYQbGDQXumCYjgxWMbbUiW6JAzCaJ9Ssss=,ssl=False,abortConnect=False");

            // Get Redis server instance
            var server = redis.GetServer("127.0.0.1", 6385);

            // Specify the pattern of keys to delete
            var keysToDelete = server.Keys(0);

            // Delete keys
            foreach (var key in keysToDelete)
            {
                try
                {
                    if (key.ToString().Contains("IOTHUB") && key.ToString().Contains("0622"))
                    {
                        redis.GetDatabase(0).KeyDelete(key);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }


           Console.WriteLine($"{keysToDelete.Count()} keys deleted.");

            return Ok();
        }
    }
}
