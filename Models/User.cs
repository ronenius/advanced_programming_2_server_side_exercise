using System.ComponentModel.DataAnnotations;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}
