using lcpsnapi.Classes;
using lcpsnapi.Context;
using lcpsnapi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lcpsnapi.Repositories
{
    public class AttachmentsRepository : IAttachments
    {
        private readonly MyDBContext _dbContext;

        public AttachmentsRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Attachments>? GetAttachments()
        {
            try
            {
                return _dbContext.Attachment?.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Attachments GetAttachmentsDetails(int id)
        {
            try
            {
                Attachments? user = _dbContext.Attachment?.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentException("Cannot get the attachment");
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddAttachments(Attachments user)
        {
            try
            {
                _dbContext.Attachment?.Add(user);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateAttachments(Attachments user)
        {
            try
            {
                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Attachments DeleteAttachments(int id)
        {
            try
            {
                Attachments? user = _dbContext.Attachment?.Find(id);

                if (user != null)
                {
                    _dbContext.Attachment?.Remove(user);
                    _dbContext.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentException("Cannot delete the attachment");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckAttachments(int id)
        {
            if (_dbContext.Attachment == null)
            {
                throw new ArgumentException("The files cannot be checked!");
            }
            
            return _dbContext.Attachment.Any(e => e.AttachmentId == id);
        }
    }
}
