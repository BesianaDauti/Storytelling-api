namespace StoryAPI1.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string ChildName { get; set; }
        public string Theme { get; set; }
        public string FavoriteAnimal { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AudioUrl { get; set; }
    }
}
