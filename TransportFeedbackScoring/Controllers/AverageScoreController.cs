using Microsoft.AspNetCore.Mvc;
using TransportFeedbackScoring.Data;
using TransportFeedbackScoring.Models;

namespace TransportFeedbackScoring.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AverageScoreController : ControllerBase

    {
        public AverageScoreController(DataService dataService)
        {
            Console.WriteLine();
            DataService = dataService;
        }

        public DataService DataService { get; }

        [HttpGet]
        public IEnumerable<AverageScore> Get()
        {
            return DataService.GetDailyAverageScores();
        }
    }
}
