namespace Employees.DtoModels
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Subordinates = new HashSet<EmployeeDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<EmployeeDto> Subordinates { get; set; }
        public int SubordinatesCount => this.Subordinates.Count;

        // Test
        // public ICollection<EmployeeDto> Test { get; set; }
    }
}