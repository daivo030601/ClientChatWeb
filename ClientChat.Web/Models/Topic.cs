namespace CleanChat.Web.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
<<<<<<< HEAD
        public string TopicName { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new List<Message>();
=======
        public string TopicName { get; set; }
        public bool Subscribed { get; set; } = false; 
>>>>>>> main
    }
}
