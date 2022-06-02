using System.ComponentModel.DataAnnotations;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class UserAPI
    {
        [Key]
        public string Username { get; set; } = null;

        public string Password { get; set; } = null;

        public string Name { get; set; } = null;

        public List<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
