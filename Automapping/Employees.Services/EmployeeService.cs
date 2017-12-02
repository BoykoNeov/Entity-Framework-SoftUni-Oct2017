namespace Employees.Services
{
    using AutoMapper;
    using Employees.Data;
    using Employees.DtoModels;
    using Employees.Models;

    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            Employee employee = context.Employees.Find(employeeId);

            EmployeeDto employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }
    }
}