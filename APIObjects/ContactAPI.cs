using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace advanced_programming_2_server_side_exercise.Models
{
    public class ContactAPI
    {
        public string id { get; set; }

        public string name { get; set; }

        public string server { get; set; }

        public string last { get; set; }

        public string lastDate { get; set; }

        public ContactAPI(string id, string name, string server, string last, DateTime? lastDate)
        {
            this.id = id;
            this.name = name;
            this.server = server;
            this.last = last;
            if (lastDate != null)
            {
                this.lastDate = ((DateTime)lastDate).ToString("o");
                this.lastDate = this.lastDate.Substring(0, this.lastDate.Length - 2);
            }
            else this.lastDate = null;
        }
    }
}
