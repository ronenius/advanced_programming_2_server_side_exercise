using System.ComponentModel.DataAnnotations;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Please Enter a number between 0-5.")]
        public int Score { get; set; }

        [Required]
        public string Feedback { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
