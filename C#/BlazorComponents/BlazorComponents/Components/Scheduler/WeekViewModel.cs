public class WeekViewModel
{
    public DateTime StartOfWeek { get; }
    public DateTime EndOfWeek => StartOfWeek.AddDays(6);
    public List<DateTime> Days { get; }

    public WeekViewModel(DateTime startOfWeek, List<DateTime> days)
    {
        StartOfWeek = startOfWeek;
        Days = days;
    }
}

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-diff).Date;
    }
}
