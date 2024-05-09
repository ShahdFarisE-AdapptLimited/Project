namespace Client.Models
{
    public class ApplicationDbContext
    {
        public List<Employee> Employees { get; set; }

        public ApplicationDbContext()
        {
            Employee employee = new Employee();
            Employee employee2 = new Employee();

            Employees = new List<Employee>()
            {
                employee, employee2
            };
        }
    }
}