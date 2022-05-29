using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class Contact
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ContactServer { get; set; }

        [Required]
        public string ContactNickname { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
