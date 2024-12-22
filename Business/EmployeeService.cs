using HRSystem.Data.Repositories;
using HRSystem.Domain.Entities;

namespace HRSystem.Business
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Role = e.Role,
                ProjectId = e.ProjectId
            });
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Çalışan bulunamadı.");

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Role = employee.Role,
                ProjectId = employee.ProjectId
            };
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Role = employeeDto.Role,
                ProjectId = employeeDto.ProjectId
            };

            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                throw new Exception("Çalışan bulunamadı.");

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Role = employeeDto.Role;
            existingEmployee.ProjectId = employeeDto.ProjectId;

            _employeeRepository.Update(existingEmployee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new Exception("Çalışan bulunamadı.");

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
