namespace TransportFeedbackScoring.Models
{
    public class AverageScore
    {
        public string Average { get; set; }
        public string Day { get; set; }
        public string TransportAgency { get; set; }

        public override string ToString()
        {
            return $"{TransportAgency}\t\t\t{Day}\t\t\t{Average}";
        }
    }
}
