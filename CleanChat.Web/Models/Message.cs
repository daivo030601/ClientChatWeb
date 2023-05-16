namespace CleanChat.Web.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; } = string.Empty;
        public int TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
    }
}
