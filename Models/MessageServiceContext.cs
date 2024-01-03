using Microsoft.EntityFrameworkCore;

namespace harjoitustyo.Models
{
    public class MessageServiceContext: DbContext
    {
        public MessageServiceContext(DbContextOptions<MessageServiceContext>options):base(options)
        {

        }
        public DbSet<Message> Message { get; set; }
        public DbSet<User> User { get; set; }
        public object Users { get; internal set; }
    }
}
