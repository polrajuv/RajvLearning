using Microsoft.AspNetCore.Mvc;
using RajvLearning.API.DTOs;
using RajvLearning.API.Entities;
using RajvLearning.API.Interfaces;

namespace RajvLearning.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LearningTopicsController : ControllerBase
{
    private readonly ILearningTopicRepository _repository;

    public LearningTopicsController(ILearningTopicRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var topics = await _repository.GetAllAsync();

        return Ok(topics);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLearningTopicDto dto)
    {
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
