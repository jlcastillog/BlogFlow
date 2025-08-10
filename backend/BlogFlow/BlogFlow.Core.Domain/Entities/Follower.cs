namespace BlogFlow.Core.Domain.Entities
{
    public class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public int BlogID { get; set;}
        public required Blog Blog { get; set; }
    }
}
