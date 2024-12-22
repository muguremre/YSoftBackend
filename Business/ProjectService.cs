using Microsoft.EntityFrameworkCore;
using HRSystem.Data.Repositories;
using HRSystem.Domain.DTOs;
using HRSystem.Domain.Entities;

namespace HRSystem.Business
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectService(IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAll()
                .Include(p => p.Employees) // Employees ilişkisini dahil ediyoruz
                .ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetAll()
                .Include(p => p.Employees) // Employees ilişkisini dahil ediyoruz
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                throw new Exception("Proje bulunamadı.");
            return project;
        }

        public async Task AddProjectAsync(ProjectDetailsDto projectDto)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                MinEmployees = projectDto.MinEmployees,
                MaxEmployees = projectDto.MaxEmployees,
                IsComplete = projectDto.IsComplete
            };

            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null)
                throw new Exception("Proje bulunamadı.");

            _projectRepository.Delete(project);
            await _projectRepository.SaveChangesAsync();
        }

        public async Task AssignEmployeeToProjectAsync(int projectId, int employeeId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new Exception($"Proje bulunamadı. Proje Id: {projectId}");

            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new Exception($"Çalışan bulunamadı. Çalışan Id: {employeeId}");

            if (project.Employees.Count >= project.MaxEmployees)
                throw new Exception("Projeye atanabilecek maksimum çalışan sayısına ulaşıldı.");

            employee.ProjectId = projectId;
            _employeeRepository.Update(employee);
            await _employeeRepository.SaveChangesAsync();
        }
    }
}
