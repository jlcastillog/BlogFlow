namespace BlogFlow.Core.Application.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int? ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public int UserId { get; set; }
    }
}
