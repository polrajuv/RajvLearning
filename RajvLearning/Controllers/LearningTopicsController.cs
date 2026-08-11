using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
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


    public LearningTopicsController(
        ILearningTopicRepository repository,
        ILogger<LearningTopicsController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    //Better approach
    [HttpGet("Welcome")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult<string> Welcome()
    {
        return Ok("Welcome to Learning Topics! portal");
    }

    // GET: api/LearningTopics
    // Public access
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation(
            "GetAll LearningTopics method is calling....");

        var topics = await _repository.GetAllAsync();

        return Ok(topics);
    }



    // POST: api/LearningTopics
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateLearningTopicDto dto)
    {

        var topic = new LearningTopic
        {
            Title = dto.Title,

            Category = dto.Category,

            Definition = dto.Definition,

            Summary = dto.Summary,

            Content = dto.Content,

            ExampleContent = dto.ExampleContent,

            Notes = dto.Notes,

            References = dto.References,

            Tags = dto.Tags,

           // Difficulty = dto.Difficulty,

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

    // GET: api/LearningTopics/{id}
    // Public access
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        var topic =
            await _repository.GetByIdAsync(id);



        if (topic == null)
        {
            return NotFound();
        }


        return Ok(topic);
    }






    // PUT: api/LearningTopics/{id}
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateLearningTopicDto dto)
    {

        var topic =
            await _repository.GetByIdAsync(id);



        if (topic == null)
        {
            return NotFound();
        }



        topic.Title = dto.Title;

        topic.Category = dto.Category;

        topic.Definition = dto.Definition;

        topic.Summary = dto.Summary;

        topic.Content = dto.Content;

        topic.ExampleContent = dto.ExampleContent;

        topic.Notes = dto.Notes;

        topic.References = dto.References;

        topic.Tags = dto.Tags;


        topic.IsPublished = dto.IsPublished;


        topic.UpdatedDate = DateTime.UtcNow;



        await _repository.UpdateAsync(topic);

        await _repository.SaveChangesAsync();



        return Ok(topic);
    }





    // DELETE: api/LearningTopics/{id}
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        var topic =
            await _repository.GetByIdAsync(id);



        if (topic == null)
        {
            return NotFound();
        }



        await _repository.DeleteAsync(id);

        await _repository.SaveChangesAsync();



        return NoContent();
    }

}