using AutoMapper;
using EmployeeManagement.Client.Services;
using EmployeeManagement.Shared;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
namespace EmployeeManagement.Client.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        [Inject]
        public IMapper Mapper { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public string PageHeader { get; set; }

        public string DepartmentId { get; set; }
        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int employeeId);

            if (employeeId != 0)
            {
                PageHeader = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeader = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();
            Mapper.Map(Employee, EditEmployeeModel);
        }
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);
            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }
        }
        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
