using System.Globalization;
using Transport_feedback_scoring.Models;

namespace Transport_feedback_scoring.Data
{

    public class DataService
    {
        static readonly string[] scores = File.ReadAllLines(@"Data\scores.txt");
        static readonly string[] referenceData = File.ReadAllLines(@"Data\reference-data.txt");

        public static IEnumerable<Route> GetRoutes()
        {
            List<Route> routes = new();

            for (int i = 0; i < 2; i++)
                foreach (string score in scores)
                {
                    var id = score.Split(" ")[0].Split(";")[1];

                    if (routes.Any((r) => r.Id == id))
                    {
                        AddSentiment(routes, score, id);
                    }
                    else
                        routes.Add(new Route { Id = id });
                }

            foreach (string refData in referenceData)
                foreach (Route r in routes)
                {
                    var id = refData.Split(";")[0].Split(" ")[0];
                    r.Name = "METRO";
                    if (r.Id == id)
                        r.TransportAgency = refData.Split(";")[1];
                }

            return routes;
        }

        private static void AddSentiment(List<Route> routes, string score, string id)
        {
            foreach (Route r in routes.Where(r => r.Id == id).ToList())
            {
                if (r.SentimentScores == null)
                    r.SentimentScores = new List<SentimentScore>
                    {
                        new SentimentScore
                            {
                                Date = DateTime.ParseExact(score.Split(";")[0], "yyyy/mm/dd", CultureInfo.InvariantCulture),
                                RouteId = id,
                                Score = int.Parse(score.Split(";")[1].Split(" ")[1])

                            }
                    };
                else
                    r.SentimentScores.Add(
                    new SentimentScore
                    {
                        Date = DateTime.ParseExact(score.Split(";")[0], "yyyy/mm/dd", CultureInfo.InvariantCulture),
                        RouteId = id,
                        Score = int.Parse(score.Split(";")[1].Split(" ")[1])

                    });
            }
        }
    }
}
