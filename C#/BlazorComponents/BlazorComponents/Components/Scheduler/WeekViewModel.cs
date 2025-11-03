namespace BlazorComponents.Components.Scheduler
{
    public class WeekViewModel
    {
        public DateTime StartOfWeek { get; }
        public List<DateTime> Days { get; }

        // Events that intersect this week
        public List<WeekEventViewModel> Events { get; }

        public WeekViewModel(DateTime startOfWeek, List<DateTime> days, List<Event> allEvents)
        {
            StartOfWeek = startOfWeek;
            Days = days;

            Events = allEvents
                .Where(ev => ev.EndDate >= StartOfWeek && ev.StartDate <= StartOfWeek.AddDays(6))
                .Select(ev => new WeekEventViewModel
                {
                    Event = ev,
                    StartIndex = Math.Max(0, (ev.StartDate - StartOfWeek).Days),
                    EndIndex = Math.Min(6, (ev.EndDate - StartOfWeek).Days)
                }).ToList();

            AssignDynamicTopOffsets(Events);
        }

        public static void AssignDynamicTopOffsets(List<WeekEventViewModel> events)
        {
            var slots = new List<int>(); // each slot stores the last day index occupied

            // Sort by start day
            var sorted = events.OrderBy(e => e.StartIndex).ToList();

            foreach (var ev in sorted)
            {
                int slotIndex = -1;

                // Find first slot where it fits
                for (int i = 0; i < slots.Count; i++)
                {
                    if (ev.StartIndex > slots[i])
                    {
                        slotIndex = i;
                        slots[i] = ev.EndIndex; // update slot end
                        break;
                    }
                }

                if (slotIndex == -1)
                {
                    // No available slot, create new
                    slots.Add(ev.EndIndex);
                    slotIndex = slots.Count - 1;
                }

                ev.TopOffset = slotIndex;
            }
        }
    }

    public class WeekEventViewModel
    {
        public Event Event { get; set; }
        public int StartIndex { get; set; } // 0-6 within week
        public int EndIndex { get; set; }   // 0-6 within week
        public int TopOffset { get; set; } = 0;
    }


    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-diff).Date;
        }
    }
}