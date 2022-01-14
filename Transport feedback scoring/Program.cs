using Transport_feedback_scoring.Data;
using Transport_feedback_scoring.Models;

var routes = DataService.GetRoutes();
var transportAgencies = new List<TransportAgency>();
var SentimentScore= new List<TransportAgency>();



Console.WriteLine("Press Enter to exit");
Console.ReadLine();