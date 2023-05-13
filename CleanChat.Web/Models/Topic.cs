namespace CleanChat.Web.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
