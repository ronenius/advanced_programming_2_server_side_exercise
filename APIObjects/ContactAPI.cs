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

        public string lastdate { get; set; }

        public ContactAPI(string id, string name, string server, string last, DateTime? lastdate)
        {
            this.id = id;
            this.name = name;
            this.server = server;
            this.last = last;
            if (lastdate != null)
            {
                this.lastdate = ((DateTime)lastdate).ToString("o");
                this.lastdate = this.lastdate.Substring(0, this.lastdate.Length - 2);
            }
            else this.lastdate = null;
        }
    }
}
