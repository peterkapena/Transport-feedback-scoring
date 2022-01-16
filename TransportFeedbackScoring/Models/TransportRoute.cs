namespace TransportFeedbackScoring.Models
{
    public class TransportRoute
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TransportAgency { get; set; }
        public List<SentimentScore> SentimentScores { get; set; }
    }
}
