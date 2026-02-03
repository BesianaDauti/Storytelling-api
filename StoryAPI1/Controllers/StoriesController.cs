using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryAPI1.Data;
using StoryAPI1.Models;
using StoryAPI1.Services;
using Microsoft.EntityFrameworkCore;

namespace StoryAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly StoryDbContext _context;
        private readonly TextToSpeechService _tts;

        public StoriesController(StoryDbContext context, TextToSpeechService tts)
        {
            _context = context;
            _tts = tts;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateStory([FromBody] UserInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) ||
                string.IsNullOrWhiteSpace(input.Theme) ||
                string.IsNullOrWhiteSpace(input.FavoriteAnimal))
            {
                return BadRequest("Please fill in all fields..");
            }

            var content = StoryGenerator.GenerateStory(input.Name, input.Theme, input.FavoriteAnimal);

            string? audioUrl = null;
            try
            {
                audioUrl = await _tts.GenerateAudioAsync(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while generating audio: {ex.Message}");
            }

            var story = new Story
            {
                ChildName = input.Name,
                Theme = input.Theme,
                FavoriteAnimal = input.FavoriteAnimal,
                Content = content,
                AudioUrl = audioUrl
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            return Ok(story);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStories()
        {
            var stories = await _context.Stories.ToListAsync();
            return Ok(stories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);

            if (story == null)
                return NotFound(new { message = "We could not found the story." });

            return Ok(story);
        }


    }
}
