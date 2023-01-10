using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IGames
    {
        public List<Games>? GetGames();
        public Games GetGamesDetails(int id);
        public void AddGames(Games games);
        public void UpdateGames(Games games);
        public Games DeleteGames(int id);
        public void ResetAIById(string? tblname = "Game", int? id = 0);
        public bool CheckGames(int id);
    }
}
