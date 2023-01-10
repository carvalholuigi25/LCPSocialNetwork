using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface ITVSeries
    {
        public List<TVSeries>? GetTVSeries();
        public TVSeries GetTVSeriesDetails(int id);
        public void AddTVSeries(TVSeries tvseries);
        public void UpdateTVSeries(TVSeries tvseries);
        public TVSeries DeleteTVSeries(int id);
        public void ResetAIById(string? tblname = "TVSerie", int? id = 0);
        public bool CheckTVSeries(int id);
    }
}
