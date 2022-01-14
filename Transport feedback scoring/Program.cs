using System.Globalization;
using Transport_feedback_scoring.Data;

var routes = DataService.GetRoutes();

foreach (var route in routes)
{
    if (route.SentimentScores.Count > 0)
    {
        var score = route.SentimentScores;
        var dailySentiments = (from s in score
                               group s by s.Date into g
                               orderby g.Average((s) => s.Score) descending
                               select new { Date = g.Key, Average = g.Average((s) => s.Score) }).ToList();
        foreach (var dailySentiment in dailySentiments)
           
            Console.WriteLine($"\t\t{route.TransportAgency}"
                + $"\t\t{dailySentiment.Date:dddd}"
                + $"\t\t{dailySentiment.Average.ToString("F", CultureInfo.InvariantCulture) }");
    }
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();