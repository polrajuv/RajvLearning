using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Configuration;
using RajvLearning.API.DTOs;
using RajvLearning.API.Entities;
using RajvLearning.API.Interfaces;

namespace RajvLearning.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LearningTopicsController : ControllerBase
{
    private readonly ILearningTopicRepository _repository;
    private readonly ILogger<LearningTopicsController> _logger;

    public LearningTopicsController(ILearningTopicRepository repository, ILogger<LearningTopicsController> logger)
    {
        _repository = repository;
            _logger = logger;
            ;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
           _logger.LogInformation("Fetching all learning topics.");

            var topics = await _repository.GetAllAsync();
            string test = null;
           var length = test.Length;

            return Ok(topics);
        }
        catch (Exception ex)
        {
            _logger.LogError("unable to Fetch all learning topics.");

            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLearningTopicDto dto)
    { ////throw new Exception("Testing Global Exception Middleware");
        var topic = new LearningTopic
        {
            Title = dto.Title,
            Description = dto.Description,
            Content = dto.Content,
            Category = dto.Category,
            Difficulty = dto.Difficulty,
            IsPublished = dto.IsPublished,
            CreatedDate = DateTime.UtcNow
        };

        await _repository.AddAsync(topic);
        await _repository.SaveChangesAsync();

       

        return CreatedAtAction(
            nameof(GetById),
            new { id = topic.Id },
            topic);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var topic = await _repository.GetByIdAsync(id);

        if (topic == null)
        {
            return NotFound();
        }

        return Ok(topic);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateLearningTopicDto dto)
    {
        var topic = await _repository.GetByIdAsync(id);

        if (topic == null)
        {
            return NotFound();
        }

        topic.Title = dto.Title;
        topic.Description = dto.Description;
        topic.Content = dto.Content;
        topic.Category = dto.Category;
        topic.Difficulty = dto.Difficulty;
        topic.IsPublished = dto.IsPublished;

        await _repository.UpdateAsync(topic);
        await _repository.SaveChangesAsync();

        return Ok(topic);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var topic = await _repository.GetByIdAsync(id);

        if (topic == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

}
