namespace Employees.Services
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Employees.Data;
    using Employees.DtoModels;
    using Employees.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto EmployeeDTObyId(int employeeId)
        {
            Employee employee = context.Employees.Find(employeeId);

            EmployeeDto employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public IList<EmployeeDto> GetEmployeesOlderThan(int age)
        {
            List<EmployeeDto> employees = this.context
                .Employees
                .Include(e => e.Manager)
                .Where(e => e.Birthday != null && (DateTime.Now.Year - e.Birthday.Value.Year) > age)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList();

            return employees;
        }

        public EmployeePersonalInfoDTO PersonalInfoById(int employeeId)
        {
            Employee employee = context.Employees.Find(employeeId);

            EmployeePersonalInfoDTO employeePersonalInfo = Mapper.Map<EmployeePersonalInfoDTO>(employee);

            return employeePersonalInfo;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            Employee employee = Mapper.Map<Employee>(employeeDto);

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public string SetBirthDay(int employeeId, DateTime date)
        {
            Employee employee = context.Employees.Find(employeeId);
            employee.Birthday = date;
            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            Employee employee = context.Employees.Find(employeeId);
            employee.Address = address;
            context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public void SetEmployeeManager(int employeeId, int managerId)
        {
            Employee employee = this.GetEmployeeByIdFromDb(employeeId);
            Employee manager = this.GetEmployeeByIdFromDb(managerId);

            employee.Manager = manager;
            context.SaveChanges();
        }

        public string GetManagerInfo(int employeeId)
        {
            Employee employee = this.GetEmployeeByIdFromDb(employeeId);
            ManagerDto managerDto = Mapper.Map<ManagerDto>(employee);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.SubordinatesCount}");

            foreach (EmployeeDto subordinate in managerDto.Subordinates)
            {
                sb.AppendLine($" - {subordinate.FirstName} {subordinate.LastName} - ${subordinate.Salary:f2}");
            }

            return sb.ToString().Trim();
        }

        private Employee GetEmployeeByIdFromDb(int employeeId)
        {
            Employee employee = this.context
                .Employees
                .Include(e => e.Subordinates)
                .SingleOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {employeeId} not found.");
            }

            return employee;
        }
    }
}