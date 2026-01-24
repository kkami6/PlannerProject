using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class Holiday : Activity
    {

        public Holiday() { }
        public Holiday(
            string name,
            DateOnly date,
            string description,
            string color = "green",
            RecurrenceType recurrence = RecurrenceType.Yearly)
            : base(name, date, description, color, recurrence)
        {
        }
        public override string GetActivityType()
        {
            return "Holiday";
        }
    }
}
