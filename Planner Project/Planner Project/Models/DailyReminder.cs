using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class DailyRemider
    {
        public string Text { get; set; }
        public enum RecurrenceType
        {
            None = 0,
            Daily = 1,
            Weekly = 2,
            Monthly = 3,
            Yearly = 4
        }
        public RecurrenceType Recurrence { get; set; }

        List<User> Users = new List<User>();
    }
}
