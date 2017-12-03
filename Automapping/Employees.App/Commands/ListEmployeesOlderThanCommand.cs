namespace Employees.App.Commands
{
    using System.Collections.Generic;
    using System.Text;
    using Employees.DtoModels;
    using Employees.Services;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService EmployeeService;

        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.EmployeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int age = int.Parse(args[0]);

            IList<EmployeeDto> employees = this.EmployeeService.GetEmployeesOlderThan(age);

            if (employees.Count == 0)
            {
                return "No such employees found.";
            }

            StringBuilder sb = new StringBuilder();

            foreach (EmployeeDto employeeDto in employees)
            {
                string managerName = "[no manager]";

                if (employeeDto.Manager != null)
                {
                    managerName = employeeDto.Manager.LastName;
                }

                sb.AppendLine(
                    $"{employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:F2} - Manager: {managerName}");
            }

            return sb.ToString().Trim();
        }
    }
}