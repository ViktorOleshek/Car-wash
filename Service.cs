using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash
{
  public class Service
  {
    public string Name
    {
      get; private set;
    }
    public double Price
    {
      get; private set;
    }

    public Service(string name, double price)
    {
      Name = name;
      Price = price;
    }
    public override string ToString()
    {
      return Name;
    }
  }
}
