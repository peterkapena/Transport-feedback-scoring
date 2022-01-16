using System.Globalization;
using TransportFeedbackScoring.Models;

namespace TransportFeedbackScoring.Data
{

    public class DataService
    {
        public DataService() { }

        readonly string[] scores = File.ReadAllLines(@"Data\scores.txt");
        readonly string[] referenceData = File.ReadAllLines(@"Data\reference-data.txt");

        private IEnumerable<TransportRoute> GetTransportRoutes()
        {
            var routes = new List<TransportRoute>();

            for (int i = 0; i < 2; i++)
                foreach (string score in scores)
                {
                    string id = score.Split(" ")[0].Split(";")[1];

                    if (routes.Any((r) => r.Id == id))
                    {
                        AddSentiments(routes, score, id);
                    }
                    else
                        routes.Add(new TransportRoute { Id = id });
                }

            foreach (string refData in referenceData)
                foreach (var r in routes)
                {
                    string id = refData.Split(";")[0].Split(" ")[0];
                    r.Name = "METRO";
                    if (r.Id == id)
                        r.TransportAgency = refData.Split(";")[1];
                }

            return routes;
        }

        private static void AddSentiments(List<TransportRoute> routes, string scorePart, string id)
        {
            foreach (var r in routes.Where(r => r.Id == id).ToList())
            {
                int score = int.Parse(scorePart.Split(";")[1].Split(" ")[1]);

                var sentimentScore = new SentimentScore()
                {
                    Date = DateTime.ParseExact(scorePart.Split(";")[0], "yyyy/mm/dd", CultureInfo.InvariantCulture),
                    RouteId = id,
                    Score = score
                };

                //  To satisfy the hypothesis 
                if (score > 0 && score < 10)
                    if (r.SentimentScores == null)
                        r.SentimentScores = new List<SentimentScore> { sentimentScore };
                    else
                        r.SentimentScores.Add(sentimentScore);
            }
        }

        public IEnumerable<AverageScore> GetDailyAverageScores()
        {
            var routes = GetTransportRoutes();
            var dailySentiments = new List<AverageScore>();

            foreach (var route in routes)
                if (route.SentimentScores.Count > 0)
                {
                    var dailySentimentsCollection = from s in route.SentimentScores
                                                    group s by s.Date into g
                                                    orderby g.Average((s) => s.Score) descending
                                                    select new AverageScore
                                                    {
                                                        Day = $"{g.Key:dddd}",
                                                        TransportAgency = route.TransportAgency,
                                                        Average = $"{g.Average((s) => s.Score).ToString("F", CultureInfo.InvariantCulture)}"
                                                    };

                    dailySentiments.AddRange(dailySentimentsCollection);
                }

            return dailySentiments;
        }
    }
}
