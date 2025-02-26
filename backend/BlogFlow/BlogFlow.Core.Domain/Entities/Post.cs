namespace BlogFlow.Core.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int? ContentId { get; set; }
        public Content? Content { get; set; }
        public string? HtmlContent { get; set; }
    }
}
