using CarWash.New_Window;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarWash
{
  /// <summary>
  /// Логика взаимодействия для MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public List<Employee> EmployeeList { get; private set; } = new List<Employee>();
    public List<Service> ServiceList { get; private set; } = new List<Service>();
    public List<Record> Data { get; private set; } = new List<Record>();
    public MainWindow()
    {
      InitializeComponent();
    }
    private void StartProgram(object sender, RoutedEventArgs e)
    {
      EmployeeList.Add(new Employee("Ярослав", 111111111));
      EmployeeList.Add(new Employee("Валентин", 222222222));
      EmployeeList.Add(new Employee("Тарас", 333333333));
      listBox_Employee.ItemsSource = EmployeeList;
      comboBox_SalaryEmployee.ItemsSource = EmployeeList;

      ServiceList.Add(new Service("Миття кузова", 50.0));
      ServiceList.Add(new Service("Миття кузова + салону", 80.0));
      ServiceList.Add(new Service("Хімчистка", 120.0));
      comboBox_ServiceType.ItemsSource = ServiceList;
    }
    private List<Date> GetServiceTime()
    {
      AddDate addElementWindow = new AddDate();
      bool? isOpen = addElementWindow.ShowDialog();
      if (isOpen == true)
      {
        var date = addElementWindow.records;
        return date;
      }
      else
      {
        MessageBox.Show("Не вдалось відкрити вікно, щоб добавити дані. " +
          "Зверніться до системного адміністратора. ");
        return null;
      }
    }

    private void Button_AddRecord(object sender, RoutedEventArgs e)
    {
      var date = GetServiceTime();
      if (listBox_Employee.SelectedItems.Count > 0)
      {
        Service selectedService = comboBox_ServiceType.SelectedItem as Service;
        if (selectedService != null && date != null)
        {
          List<Employee> selectedEmployees = listBox_Employee.SelectedItems.Cast<Employee>().ToList();

          // Створення запису з усіма вибраними працівниками
          Record record = new Record(selectedEmployees, selectedService, date [0], date [1]);
          Data.Add(record);
        }
        else
        {
          MessageBox.Show("Не вибрано послугу або час");
        }
      }
      else
      {
        MessageBox.Show("Не вибрано працівників");
      }
    }

    private void ListBox_Employee_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      // Отримання вибраних елементів
      foreach (Employee selectedEmployee in listBox_Employee.SelectedItems)
      {
        // Ваш код для обробки кожного вибраного працівника
      }
    }
    private void ComboBox_ServiceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      Service selectedService = comboBox_ServiceType.SelectedItem as Service;
    }

    private void Button_AddEmployee(object sender, RoutedEventArgs e)
    {
      AddEmployee addElementWindow = new AddEmployee();
      bool? isOpen = addElementWindow.ShowDialog();

      if (isOpen == true)
      {
        Employee newEntry = addElementWindow.NewEmployee;
        EmployeeList.Add(newEntry);
        listBox_Employee.Items.Refresh();
        comboBox_SalaryEmployee.Items.Refresh();
      }
      else
      {
        MessageBox.Show("Не вдалось відкрити вікно, щоб добавити дані. " +
          "Зверніться до системного адміністратора. ");
      }
    }
    private void Button_AddService(object sender, RoutedEventArgs e)
    {
      AddService addElementWindow = new AddService();
      bool? isOpen = addElementWindow.ShowDialog();

      if (isOpen == true)
      {
        Service newEntry = addElementWindow.NewService;
        ServiceList.Add(newEntry);
        comboBox_SalaryEmployee.Items.Refresh();
      }
      else
      {
        MessageBox.Show("Не вдалось відкрити вікно, щоб добавити дані. " +
          "Зверніться до системного адміністратора. ");
      }
    }

    private List<Record> CopyCurrentData()
    {
      var tmp = new List<Record>();
      Data.ForEach(tmp.Add);
      return tmp;
    }
    private void MenuItem_Open(object sender, RoutedEventArgs e)
    {
      string path = GetFilePath();
      if (path == null)
      {
        MessageBox.Show("Файл не було вибрано.");
        return;
      }

      List<Record> tmp = CopyCurrentData();
      Data.Clear();

      try
      {
        ReadDataFromFile(path);
      }
      catch (Exception ex)
      {
        // Відновлення попередніх даних у випадку помилки
        tmp.ForEach(Data.Add);

        MessageBox.Show($"Помилка при читанні файлу: {ex.Message}\n" +
                        $"Приклад заповнення: Валентин 222222222 Тарас " +
                        $"333333333, Хімчистка 120, 2.12.2019, 1:31, " +
                        $"2.12.2023, 1:31\r\n");
      }
    }
    private void ReadDataFromFile(string path)
    {
      if (File.Exists(path) && new FileInfo(path).Length > 0)
      {
        foreach (string line in File.ReadLines(path))
        {
          ProcessFileLine(line);
        }
      }
      else
      {
        MessageBox.Show("Файл порожній або не існує.");
      }
    }

    private void ProcessFileLine(string line)
    {
      string [] parts = line.Split(',');

      if (parts.Length == 6)
      {
        List<Employee> employees = new List<Employee>();
        string [] employeeInfo = parts [0].Split(' ');
        for (int i = 0; i < employeeInfo.Length; i += 2)
        {
          if (double.TryParse(employeeInfo [i + 1], out double phoneNumber))
          {
            employees.Add(new Employee
              (employeeInfo [i].Trim(), phoneNumber));
          }
        }


        string [] serviceInfo = parts [1].Split(' ');
        serviceInfo = serviceInfo.Where(s => !string.IsNullOrEmpty(s)).ToArray();
        Service service = ServiceList.FirstOrDefault(s => s.Name == serviceInfo [0]);


        string [] startTimeInfo = parts [2].Split('.')
          .Concat(parts [3].Split(':')).ToArray();
        string [] endTimeInfo = parts [4].Split('.')
          .Concat(parts [5].Split(':')).ToArray();

        if (employees.Count > 0 && service != null &&
            startTimeInfo.Length == 5 && endTimeInfo.Length == 5)
        {
          int startYear, startMonth, startDay, startHour, startMinute;
          int endYear, endMonth, endDay, endHour, endMinute;

          if (int.TryParse(startTimeInfo [0], out startDay) &&
              int.TryParse(startTimeInfo [1], out startMonth) &&
              int.TryParse(startTimeInfo [2], out startYear) &&
              int.TryParse(startTimeInfo [3], out startHour) &&
              int.TryParse(startTimeInfo [4], out startMinute) &&
              int.TryParse(endTimeInfo [0], out endDay) &&
              int.TryParse(endTimeInfo [1], out endMonth) &&
              int.TryParse(endTimeInfo [2], out endYear) &&
              int.TryParse(endTimeInfo [3], out endHour) &&
              int.TryParse(endTimeInfo [4], out endMinute))
          {
            Date startTime = new Date(startDay, startMonth, startYear, startHour, startMinute);
            Date endTime = new Date(endDay, endMonth, endYear, endHour, endMinute);

            // Створення запису
            Record record = new Record(employees, service, startTime, endTime);
            Data.Add(record);
          }
          else
          {
            MessageBox.Show("Помилка при парсингу дати та часу.");
          }
        }
        else
        {
          throw new Exception("Некоректні дані у файлі.");
        }

      }
    }
    private string GetFilePath()
    {
      var fileDialog = new Microsoft.Win32.OpenFileDialog();
      return fileDialog.ShowDialog() == true ? fileDialog.FileName : null;
    }
    private void MenuItem_Save(object sender, RoutedEventArgs e)
    {
      string path = GetFilePath();
      if (path == null)
      {
        MessageBox.Show("Файл не було вибрано.");
        return;
      }

      try
      {
        SaveDataToFile(path);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Помилка при записі у файл: {ex.Message}");
      }
    }
    private void SaveDataToFile(string path)
    {
      File.WriteAllText(path, string.Empty);

      using (StreamWriter file = new StreamWriter(path))
      {
        foreach (var element in Data)
        {
          string employeeNames = string.Join(" ", element
            .Employees.Select(e => $"{e.Name} {e.PhoneNumber}"));

          string serviceInfo = $"{element.Service.Name} {element.Service.Price}";

          string startTimeInfo = $"{element.StartTime.Day}.{element.StartTime.Month}." +
                                  $"{element.StartTime.Year}, {element.StartTime.Hour}:" +
                                  $"{element.StartTime.Minute}";

          string endTimeInfo = $"{element.EndTime.Day}.{element.EndTime.Month}." +
                                $"{element.EndTime.Year}, {element.EndTime.Hour}:" +
                                $"{element.EndTime.Minute}";

          file.WriteLine($"{employeeNames}, {serviceInfo}, " +
            $"{startTimeInfo}, {endTimeInfo}");
        }
      }
    }
    private void MenuItem_Help(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Email system admin: Viktor.Oleshek.PZ.2022@lpnu.ua");
    }
    private void MenuItem_Exit(object sender, RoutedEventArgs e)
    {
      Environment.Exit(0);
    }

    private void Button_CalculateSalary(object sender, RoutedEventArgs e)
    {
      Employee selectedEmployee = comboBox_SalaryEmployee.SelectedItem as Employee;
      DateTime? selectedDateTime = datePicker.SelectedDate;

      if (selectedEmployee != null && selectedDateTime.HasValue)
      {
        // Перетворення DateTime? на Date
        Date selectedDate = new Date(
            selectedDateTime.Value.Day,
            selectedDateTime.Value.Month,
            selectedDateTime.Value.Year,
            selectedDateTime.Value.Hour,
            selectedDateTime.Value.Minute);

        var employeeRecords = Data.Where(record => record.Employees
            .Any(employee => employee.Name == selectedEmployee.Name)
            && record.StartTime.Year == selectedDate.Year
            && record.StartTime.Month == selectedDate.Month
            && record.StartTime.Day == selectedDate.Day);

        double totalSalary = 0;
        foreach (var record in employeeRecords)
        {
          double servicePrice = record.Service.Price;
          int numberOfEmployees = record.Employees.Count;

          double individualSalary = servicePrice / 2 / numberOfEmployees;

          totalSalary += individualSalary;
        }
        MessageBox.Show($"Зарплата працівника {selectedEmployee.Name} за {selectedDate.Day}." +
          $"{selectedDate.Month}.{selectedDate.Year}: {totalSalary}");
      }
      else
      {
        MessageBox.Show("Виберіть працівника та дату.");
      }
    }
  }
}

