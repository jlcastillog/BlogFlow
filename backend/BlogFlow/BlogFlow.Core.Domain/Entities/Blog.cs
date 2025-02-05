using BlogFlow.Core.Domain.Entities;
using System.Reflection.Metadata;

namespace BlogFlow.Core.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
