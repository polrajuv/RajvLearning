
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.DTOs;

namespace RajvLearning.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        var dashboard = new DashboardDto
        {
            TotalTopics = await _context.LearningTopic.CountAsync(),
            TotalUsers = await _context.Users.CountAsync(),

            TotalPublished = await _context.LearningTopic
                            .Where(x => x.IsPublished)
                            .CountAsync(),

            TotalCategories = await _context.LearningTopic
                            .Where(x => !string.IsNullOrEmpty(x.Category))
                            .Select(x => x.Category)
                            .Distinct()
                            .CountAsync()
                            };

        return Ok(dashboard);
    }
}

