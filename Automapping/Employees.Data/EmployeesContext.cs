namespace Employees.Data
{
    using Microsoft.EntityFrameworkCore;
    using Employees.Models;
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee>
    }
}