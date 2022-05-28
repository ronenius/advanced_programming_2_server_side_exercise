namespace advanced_programming_2_server_side_exercise.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsSent { get; set; }
        public DateTime Created { get; set; }
    }
}
