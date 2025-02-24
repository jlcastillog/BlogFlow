namespace BlogFlow.Core.Application.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? HtmlContent { get; set; }
        public int BlogId { get; set; }
    }
}
