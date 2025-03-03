using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        public WeatherForecastController()
        {
            var logger = new Logger();

            logger.WriteEvent("��������� � ������� � ���������");
            logger.WriteError("��������� �� ������ � ���������");
        }
    }
}
