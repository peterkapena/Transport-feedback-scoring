using System.Globalization;

namespace Transport_feedback_scoring.Models
{
    public class Route
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<SentimentScore> SentimentScores { get; set; }
    }
}
