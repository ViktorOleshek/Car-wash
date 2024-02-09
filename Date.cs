using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash
{
  public class Date
  {
    public int Day
    {
      get; private set;
    }
    public int Month
    {
      get; private set;
    }
    public int Year
    {
      get; private set;
    }
    public int Hour
    {
      get; private set;
    }
    public int Minute
    {
      get; private set;
    }

    public Date(int day, int month, int year, int hour, int minute)
    {
      Day = day;
      Month = month;
      Year = year;
      Hour = hour;
      Minute = minute;
    }

    public int CompareTo(Date other)
    {
      if (ReferenceEquals(this, other))
        return 0;

      if (ReferenceEquals(null, other))
        return 1;

      // Порівняння років
      if (Year != other.Year)
        return Year.CompareTo(other.Year);

      // Порівняння місяців
      if (Month != other.Month)
        return Month.CompareTo(other.Month);

      // Порівняння днів
      if (Day != other.Day)
        return Day.CompareTo(other.Day);

      // Порівняння годин
      if (Hour != other.Hour)
        return Hour.CompareTo(other.Hour);

      // Порівняння хвилин
      if (Minute != other.Minute)
        return Minute.CompareTo(other.Minute);

      return 0; // Об'єкти рівні за всіма компонентами
    }
  }
}
