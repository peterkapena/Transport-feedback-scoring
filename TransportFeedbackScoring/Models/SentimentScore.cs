namespace TransportFeedbackScoring.Models
{
    public class SentimentScore
    {
        public DateTime Date { get; set; }
        public string RouteId { get; set; }
        public int Score { get; set; }
    }
}
