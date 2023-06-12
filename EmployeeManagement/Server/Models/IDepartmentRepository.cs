using EmployeeManagement.Shared;

namespace EmployeeManagement.Server.Models
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int departmentId);
    }
}
