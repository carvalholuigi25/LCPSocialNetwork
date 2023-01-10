using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IMovies
    {
        public List<Movies>? GetMovies();
        public Movies GetMoviesDetails(int id);
        public void AddMovies(Movies movies);
        public void UpdateMovies(Movies movies);
        public Movies DeleteMovies(int id);
        public void ResetAIById(string? tblname = "Movie", int? id = 0);
        public bool CheckMovies(int id);
    }
}
