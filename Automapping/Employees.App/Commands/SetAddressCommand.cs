namespace Employees.App.Commands
{
    using Employees.Services;
    using System.Linq;

    internal class SetAddressCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetAddressCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            string employeeAddress = string.Join(" ", args.Skip(1));

            string employeeName = employeeService.SetAddress(employeeId, employeeAddress);

            return $"{employeeName} address was successfully set to {employeeAddress}";
        }
    }
}
