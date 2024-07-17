namespace BlogApp.Models.OutputModels
{
    public abstract class PostInfoBase : PostBase
    {
        public string Content { get; set; }
        public string PostImageContent { get; set; }
        public string PostImageName { get; set; }
        public UserProfile User { get; set; }
        public PostCategory Category { get; set; }
    }
}
