import { useEffect, useState } from "react";

export function Home() {
  const [averageScores, setAverageScores] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async () => {
      await populateWeatherData();
    })();
  }, []);

  async function populateWeatherData() {
    const r = await fetch("averagescore");
    const d = await r.json();

    console.log(d);
    setAverageScores(d);
    setLoading(false);
  }
  return (
    <div>
      <h1>Scoring.</h1>
      <p>Average scores for each route, per day of the week.</p>
      {loading ? (
        "...loading"
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Transport agency</th>
              <th>Day of week</th>
              <th>Average score</th>
            </tr>
          </thead>
          <tbody>
            {averageScores.map((averageScore, i) => (
              <tr key={i}>
                <td>{averageScore.transportAgency}</td>
                <td>{averageScore.day}</td>
                <td>{averageScore.average}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
