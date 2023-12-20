using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSystem10LabaKapetz
{
    public interface ICrudHr
    {
        List<Employee> ReadAllEmployee();
        Position ReadPositionById(int id);
        void Create(Employee employee);
        bool UserExistsById(int id);
        List<Employee> ReadById(int id);
        List<Employee> ReadByFirstName(string firstName);
        List<Employee> ReadByUserId(int userId);
        List<Employee> ReadByLastName(string lastName);
        List<Employee> ReadByPatronymic(string patronymic);
        List<Employee> ReadByBirthDate(DateTime birthdate);
        List<Employee> ReadBySalary(double salary);
        List<Employee> ReadByPassport(string passport);
        Employee Read(int employeePos);
        void Update(int employeePos, int id, int userId, string firstName, string lastName, string patronymic,
            DateTime birthDate, double salary, string passport);
        void Delete(Employee employee);
    }
}
