using Microsoft.AspNetCore.Mvc;
using SW.Classes;
using SW.Services;

namespace FirstAngular.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StarShipController : ControllerBase
    {
        private readonly ILogger<StarShipController> _logger;
        private readonly ISwApiServices _swServices;
        public StarShipController(ILogger<StarShipController> logger, ISwApiServices swApiServices)
        {
            _logger = logger;
            _swServices = swApiServices;
        }
        [HttpGet]
        public IEnumerable<Starship> Get(int distance)
        {

            return _swServices.GetStarshipswithNumberofStops(distance).ToArray();
        }
    }
}