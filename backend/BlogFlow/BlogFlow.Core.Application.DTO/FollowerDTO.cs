namespace BlogFlow.Core.Application.DTO
{
    public class FollowerDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required UserDTO User { get; set; }
        public int BlogID { get; set; }
        public required BlogDTO Blog { get; set; }
    }
}
