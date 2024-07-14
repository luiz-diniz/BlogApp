using BlogApp.Models.Enums;

namespace BlogApp.Models.OutputModels
{
    public class PostReviewInfo : PostBase
    {
        public string Username { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}