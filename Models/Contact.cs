using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string ContactUsername { get; set; }

        public string ContactServer { get; set; }

        public string ContactNickname { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
