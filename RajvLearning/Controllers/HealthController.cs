using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;

namespace RajvLearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("db")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();

                if (canConnect)
                    return Ok(new
                    {
                        Success = true,
                        Message = "SQL Server connection successful."
                    });

                return BadRequest(new
                {
                    Success = false,
                    Message = "Unable to connect to SQL Server."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}