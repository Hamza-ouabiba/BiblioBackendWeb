using BiblioBackendWeb.Models;
using BiblioBackendWeb.Repository.Implementations;
using BiblioBackendWeb.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiblioBackendWeb.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAuteur")]
        public IEnumerable<Auteur> Auteur()
        {
            using (UnitOfWork uow = new UnitOfWork(new BibliothequeDbContext()))
            {
                return uow.Auteur.GetAll();
            }
        }
    }
}
