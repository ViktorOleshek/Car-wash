using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWash
{
  public class Record
  {
    public List<Employee> Employees
    {
      get; private set;
    }
    public Service Service
    {
      get; private set;
    }
    public Date StartTime
    {
      get; private set;
    }
    public Date EndTime
    {
      get; private set;
    }

    public Record(List<Employee> employees, Service service, Date startTime, Date endTime)
    {
      Employees = employees;
      Service = service;
      StartTime = startTime;
      EndTime = endTime;
    }

    // Конструктор для одного працівника
    public Record(Employee employee, Service service, Date startTime, Date endTime)
        : this(new List<Employee> { employee }, service, startTime, endTime)
    {
    }
  }
}
