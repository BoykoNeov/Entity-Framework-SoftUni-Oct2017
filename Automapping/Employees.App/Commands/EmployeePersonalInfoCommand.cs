namespace Employees.App.Commands
{
    using Employees.DtoModels;
    using Employees.Services;
    using System;
    using System.Globalization;

    internal class EmployeePersonalInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeePersonalInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // <employeeId>
        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);

            EmployeePersonalInfoDTO employeeInfoDto = employeeService.PersonalInfoById(employeeId);

            string birthdayOutput = employeeInfoDto.Birthday.HasValue ? employeeInfoDto.Birthday.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture) : "No birthday entered";

            string result = $"ID: {employeeInfoDto.Id} - {employeeInfoDto.FirstName} {employeeInfoDto.LastName} - ${employeeInfoDto.Salary:f2}" + Environment.NewLine +
                $"Birthday: {birthdayOutput}" + Environment.NewLine +
                $"Address: {employeeInfoDto.Address ?? "No address entered"}";

            return result;
        }
    }
}