using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IMedia
    {
        public List<Media>? GetFiles();
        public Media GetFilesDetails(int id);
        public void AddFiles(Media media);
        public void UpdateFiles(Media media);
        public Media DeleteFiles(int id);
        public bool CheckFiles(int id);
    }
}
