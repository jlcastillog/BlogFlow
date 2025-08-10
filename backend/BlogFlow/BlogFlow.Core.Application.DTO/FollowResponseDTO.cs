namespace BlogFlow.Core.Application.DTO
{
    public class FollowResponseDTO
    {
        public int NumberOfFollowers { get; set; }

        public FollowerDTO[] Followers { get; set; } = Array.Empty<FollowerDTO>();
    }
}
