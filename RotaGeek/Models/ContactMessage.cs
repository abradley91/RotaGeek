using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RotaGeek.Models
{
    public class ContactMessage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
