namespace BlogFlow.Core.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
