using System;
using System.Globalization;

public static class HijriDateConverter
{
    // Expected input format: "yyyy/MM/dd" or "yyyy-MM-dd"
    public static DateTime ToGregorian(string hijriDate)
    {
        if (string.IsNullOrWhiteSpace(hijriDate))
            throw new ArgumentNullException(nameof(hijriDate));

        // Normalize to yyyy/MM/dd
        hijriDate = hijriDate.Replace("-", "/");

        var hijri = new HijriCalendar();
        var culture = new CultureInfo("ar-SA")
        {
            DateTimeFormat = { Calendar = hijri }
        };

        if (DateTime.TryParseExact(hijriDate, "yyyy/MM/dd", culture, DateTimeStyles.None, out var gregorian))
        {
            return gregorian;
        }

        throw new FormatException($"Invalid Hijri date format: {hijriDate}");
    }
}