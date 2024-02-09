using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace CarWash.New_Window
{
  /// <summary>
  /// Логика взаимодействия для AddDate.xaml
  /// </summary>
  public partial class AddDate : Window
  {
    public List<Date> records = new List<Date>();
    public AddDate()
    {
      InitializeComponent();
    }
    private Date GetDate(DateTimeUpDown dateTimeUpDown)
    {
      DateTime? selectedDateTime = dateTimeUpDown.Value;

      if (selectedDateTime.HasValue)
      {
        int day = selectedDateTime.Value.Day;
        int month = selectedDateTime.Value.Month;
        int year = selectedDateTime.Value.Year;
        int hour = selectedDateTime.Value.Hour;
        int minute = selectedDateTime.Value.Minute;
        return new Date(day, month, year, hour, minute);
      }
      else
      {
        Console.WriteLine("No DateTime selected.");
        return null;
      }
    }

    private void Button_SaveDate(object sender, RoutedEventArgs e)
    {
      Date startTime = GetDate(dateTimeUpDown_startTime);
      Date endTime = GetDate(dateTimeUpDown_endTime);

      if (startTime == null || endTime == null)
      {
        System.Windows.MessageBox.Show("Всі поля повинні бути заповнені");
        return;
      }

      if (endTime.CompareTo(startTime) <= 0)
      {
        System.Windows.MessageBox.Show("Час закінчення повинен бути більше за час початку");
        return;
      }

      DateTime dateTime = DateTime.Now;
      Date dateNow = new Date(dateTime.Day, dateTime.Month, dateTime.Year,
        dateTime.Hour, dateTime.Minute);
      if (startTime.CompareTo(dateNow) > 0 || endTime.CompareTo(dateNow) > 0)
      {
        System.Windows.MessageBox.Show("Дата і час не повинні бути більше поточної дати і часу");
        return;
      }

      records.Add(startTime);
      records.Add(endTime);
      DialogResult = true;
      return;
    }
  }
}
