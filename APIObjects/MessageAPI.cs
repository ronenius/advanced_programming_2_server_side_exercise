namespace advanced_programming_2_server_side_exercise.Models
{
    public class MessageAPI
    {
        public int? Id { get; set; }
        public string Content { get; set; }
        public bool IsSent { get; set; }
        public DateTime Created { get; set; }

        public MessageAPI(int? id, string content, bool isSent, DateTime created)
        {
            Id = id;
            Content = content;
            IsSent = isSent;
            Created = created;
        }
    }
}
