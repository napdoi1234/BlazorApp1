using EmployeeManagement.Client.Services;
using EmployeeManagement.Shared;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Client.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
        protected int SelectedEmployeesCount { get; set; } = 0;

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }
        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
    }
}