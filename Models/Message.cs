using System.ComponentModel.DataAnnotations;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class Message
    {

        public int Id { get; set; }

        [Required]
        public string FromUsername { get; set; }

        [Required]
        public string ToUsername { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Created { get; set; }
    }
}