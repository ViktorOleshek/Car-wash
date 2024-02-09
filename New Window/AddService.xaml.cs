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
  /// Логика взаимодействия для AddService.xaml
  /// </summary>
  public partial class AddService : Window
  {
    public Service NewService
    {
      get; private set;
    }
    public AddService()
    {
      InitializeComponent();
    }
    private bool ValidateDate(string name, string price)
    {
      // Перевірка імені
      if (ContainsDigit(name))
      {
        System.Windows.MessageBox.Show("Ім'я не повинно містити цифр");
        return false;
      }

      // Перевірка номеру телефону
      if (ContainsNonNumericOrSpace(price))
      {
        System.Windows.MessageBox.Show("Ціна не повинна містити розділових знаків, букв або пробілів");
        return false;
      }

      return true;
    }
    private bool ContainsNonNumericOrSpace(string input)
    {
      return input.Any(c => !char.IsDigit(c) && !char.IsWhiteSpace(c));
    }
    private bool ContainsDigit(string input)
    {
      return input.Any(char.IsDigit);
    }
    private void Button_SaveDate(object sender, RoutedEventArgs e)
    {
      string name = TextBox_Name.Text;
      string priceText = TextBox_Price.Text;

      if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText))
      {
        System.Windows.MessageBox.Show("Всі поля повинні бути заповнені");
        return;
      }

      if (ValidateDate(name, priceText))
      {
        if (double.TryParse(priceText, out double price))
        {
          NewService = new Service(name, price);
          DialogResult = true;
        }
        else
        {
          System.Windows.MessageBox.Show("Некоректний номер телефону");
        }
      }
    }
  }
}
