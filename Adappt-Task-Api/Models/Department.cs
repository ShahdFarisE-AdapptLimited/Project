using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        //Navigation property 
       public ICollection<Employee>? Employees { get; set; } = new Collection<Employee>();
    }
}
