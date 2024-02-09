using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash
{
  public class Employee
  {
    public string Name
    {
      get; private set;
    }
    public double PhoneNumber
    {
      get; private set;
    }

    public Employee(string name, double phoneNumber)
    {
      Name = name;
      PhoneNumber = phoneNumber;
    }

    public override string ToString()
    {
      return Name;
    }
  }

}
