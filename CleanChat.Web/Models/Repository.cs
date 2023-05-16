namespace CleanChat.Web.Models
{
    public class Repository
    {
        public List<Topic> topics = new List<Topic>();
        public Topic NewTopic { get; set; } = new Topic();
    }
}
