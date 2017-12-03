namespace Employees.App.Commands
{
    using Employees.Services;

    public class SetManagerCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetManagerCommand(EmployeeService empService)
        {
            this.employeeService = empService;
        }

        public string Execute(params string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            this.employeeService.SetEmployeeManager(employeeId, managerId);

            return $"Employee with ID {employeeId}'s manager is now employee with ID {managerId}.";
        }
    }
}