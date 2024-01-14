using Microsoft.AspNetCore.Mvc;

namespace CrossyRoad2D.Server.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _validApiKey;

        public AdminController(IConfiguration configuration)
        {
            _validApiKey = configuration.GetValue<string>("AdminApiKey");
        }

        [HttpPost]
        public IActionResult ExecuteCommand()
        {
            var apiKey = Request.Headers["X-Api-Key"];
            if(apiKey != _validApiKey)
            {
                return new UnauthorizedResult();
            }

            return new OkResult();
        }
    }
}
