using HRSystem.Business;
using HRSystem.Domain.DTOs;
using HRSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HRSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: api/project
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        // GET: api/project/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/project
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody] ProjectDetailsDto projectDto)
        {
            try
            {
                await _projectService.AddProjectAsync(projectDto);
                return CreatedAtAction(nameof(GetProjectById), new { id = projectDto.Name }, projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        // PUT: api/project/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            try
            {
                project.Id = id;
                await _projectService.UpdateProjectAsync(project);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteProjectAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        // POST: api/project/{projectId}/assign-employee/{employeeId}
        [HttpPost("{projectId}/assign-employee/{employeeId}")]
        public async Task<IActionResult> AssignEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                await _projectService.AssignEmployeeToProjectAsync(projectId, employeeId);
                return Ok(new { Message = "Çalışan projeye başarıyla atandı." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

    }
}
