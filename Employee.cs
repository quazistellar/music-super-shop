using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public class Employee
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public double Salary { get; set; }
        public string Passport { get; set; }


        public Employee(int id, int userId, string firstName, string lastName, string patronymic, DateTime birthDate, double salary, string passport)
        {
            ID = id;
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
            BirthDate = birthDate;
            Passport = passport;
            Salary = salary;
            this.UserID = userId;
        }
    }
}
