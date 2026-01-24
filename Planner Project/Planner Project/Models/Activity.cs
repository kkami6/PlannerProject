namespace BusinessLayer.Models
{
    public abstract class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; } = null!;
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public string Color { get; set; } = null!;

        public enum RecurrenceType
        {
            None = 0,
            Daily = 1,
            Weekly = 2,
            Monthly = 3,
            Yearly = 4
        }

        public RecurrenceType Recurrence { get; set; }

        protected Activity() { }

        protected Activity(
            string name,
            DateOnly date,
            string description,
            string color,
            RecurrenceType recurrence)
        {
            Name = name;
            Description = description;
            Color = color;
            Recurrence = recurrence;
            Date = date;
        }

        public abstract string GetActivityType();
    }
}
