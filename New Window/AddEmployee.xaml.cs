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

namespace CarWash.New_Window
{
  /// <summary>
  /// Логика взаимодействия для AddEmployee.xaml
  /// </summary>
  public partial class AddEmployee : Window
  {
    public Employee NewEmployee
    {
      get; private set;
    }
    public AddEmployee()
    {
      InitializeComponent();
    }

    private bool ValidateDate(string name, string phoneNumberText)
    {
      // Перевірка імені
      if (ContainsDigit(name))
      {
        MessageBox.Show("Ім'я не повинно містити цифр.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
      }

      // Перевірка номеру телефону
      if (ContainsNonNumeric(phoneNumberText))
      {
        MessageBox.Show("Номер телефону повинен містити тільки цифри.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
      }

      return true;
    }

    private bool ContainsNonNumeric(string input)
    {
      return input.Any(c => !char.IsDigit(c));
    }

    private bool ContainsDigit(string input)
    {
      return input.Any(char.IsDigit);
    }

    private void Button_SaveDate(object sender, RoutedEventArgs e)
    {
      string name = TextBox_Name.Text;
      string phoneNumberText = TextBox_PhoneNumber.Text;

      if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phoneNumberText))
      {
        MessageBox.Show("Всі поля повинні бути заповнені.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
      }

      if (ValidateDate(name, phoneNumberText))
      {
        if (double.TryParse(phoneNumberText, out double phoneNumber))
        {
          NewEmployee = new Employee(name, phoneNumber);
          DialogResult = true;
        }
        else
        {
          MessageBox.Show("Некоректний номер телефону.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
    }

  }
}
