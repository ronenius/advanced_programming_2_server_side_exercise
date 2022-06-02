namespace advanced_programming_2_server_side_exercise.Models
{
    public class MessageAPI
    {
        public int? Id { get; set; } = null;
        public string Content { get; set; } = null;
        public bool Sent { get; set; }
        public DateTime Created { get; set; }

        public MessageAPI(int? id, string content, bool sent, DateTime created)
        {
            Id = id;
            Content = content;
            Sent = sent;
            Created = created;
        }
    }
}
