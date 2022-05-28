namespace advanced_programming_2_server_side_exercise.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public string Feedback { get; set; }

        public string Name { get; set; }

        public DateTime DateTime { get; set; }
    }
}
