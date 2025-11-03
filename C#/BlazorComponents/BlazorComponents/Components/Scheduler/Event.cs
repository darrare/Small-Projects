namespace BlazorComponents.Components.Scheduler
{
    public class Event
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; } = "#0094ff"; // default blue
    }
}
