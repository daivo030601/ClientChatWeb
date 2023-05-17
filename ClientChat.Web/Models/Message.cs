using System.ComponentModel.DataAnnotations;

namespace CleanChat.Web.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public DateTime SentDate { get; set; }
        [Required]
        public string Content { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }
        public int TopicId { get; set; }
    }
}
