namespace BlogFlow.Core.Domain.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
