using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RajvLearning.API.Data;
using RajvLearning.API.DTOs.JWTAuth;
using RajvLearning.API.Entities;
using RajvLearning.API.Services;


namespace RajvLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly ILogger<JWTAuthController> _logger;


        public JWTAuthController(
            AppDbContext context,
            ITokenService tokenService,
            ILogger<JWTAuthController> logger)
        {
            _context = context;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            _logger.LogInformation(
                "Register request received for {Email}",
                request.Email);


            var existingUser = await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == request.Email);


            if (existingUser != null)
            {
                return BadRequest(
                    "User already exists.");
            }


            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Role = request.Role
            };


            var passwordHasher =
                new PasswordHasher<User>();


            user.PasswordHash =
                passwordHasher.HashPassword(
                    user,
                    request.Password);


            _context.Users.Add(user);

            await _context.SaveChangesAsync();


            return Ok(new
            {
                message = "User registered successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginRequestDto request)
        {
            _logger.LogInformation(
                "Login attempt for email: {Email}",
                request.Email);


            var user = await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == request.Email);


            if (user == null)
            {
                _logger.LogWarning(
                    "Login failed. User not found: {Email}",
                    request.Email);

                return Unauthorized(
                    "Invalid email or password.");
            }


            var passwordHasher =
                new PasswordHasher<User>();


            var result =
                passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);


            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning(
                    "Login failed. Invalid password for: {Email}",
                    request.Email);

                return Unauthorized(
                    "Invalid email or password.");
            }


            var token =
                _tokenService.CreateToken(user);


            _logger.LogInformation(
                "Login successful for user: {Email}",
                user.Email);


            return Ok(new TokenResponseDto
            {
                Token = token,
                Expiration =
                    DateTime.UtcNow.AddMinutes(
                        60)
            });
        }


    }
}