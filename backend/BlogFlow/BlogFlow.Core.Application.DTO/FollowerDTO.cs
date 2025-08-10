namespace BlogFlow.Core.Application.DTO
{
    public class FollowerDTO
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public UserDTO? User { get; set; }
        public required int BlogID { get; set; }
        public BlogDTO? Blog { get; set; }
    }
}
