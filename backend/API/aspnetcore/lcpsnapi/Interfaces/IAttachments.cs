using lcpsnapi.Classes;

namespace lcpsnapi.Interfaces
{
    public interface IAttachments
    {
        public List<Attachments>? GetAttachments();
        public Attachments GetAttachmentsDetails(int id);
        public void AddAttachments(Attachments file);
        public void UpdateAttachments(Attachments file);
        public Attachments DeleteAttachments(int id);
        public bool CheckAttachments(int id);
    }
}
