//namespace StoryAPI1.Services
//{
//    public class StoryGenerator
//    {
//        public static string GenerateStory(string name, string theme, string animal)
//        {
//            return theme.ToLower() switch
//            {
//                "adventure" => $"On a sunny day, {name} went on an adventure with a brave {animal}. They faced challenges in a magical forest and discovered an ancient secret that glowed in the dark.",
//                "mystery" => $"One foggy evening, {name} and their clever {animal} found a hidden key buried beneath an old tree. As they followed the clues, they uncovered a secret room that hadn’t been opened in a hundred years.",
//                "fantasy" => $"In a magical forest, {name} and a talking {animal} met a wise wizard who gave them a glowing crystal to save their village from darkness.",
//                "space" => $"Floating among the stars, {name} and their brave {animal} boarded a shiny spaceship to explore unknown planets and make new alien friends.",
//                "nature" => $"One peaceful afternoon, {name} and their gentle {animal} wandered through blooming meadows and helped a lost baby bird find its way home.",

//                _ => "No stories were found for this topic, please choose another topic."
//            }; 
//        }
//    }
//}
using System;
using System.Collections.Generic;

namespace StoryAPI1.Services
{
    public class StoryGenerator
    {
        private static readonly Random rnd = new Random();

        public static string GenerateStory(string name, string theme, string animal)
        {
            name = string.IsNullOrWhiteSpace(name) ? "Someone" : name.Trim();
            animal = string.IsNullOrWhiteSpace(animal) ? "animal" : animal.Trim();
            theme = string.IsNullOrWhiteSpace(theme) ? "" : theme.Trim().ToLower();

            var stories = new Dictionary<string, List<string>>
            {
                ["adventure"] = new List<string>
                {
                    $"On a sunny day, {name} went on an adventure with a brave {animal}. They faced challenges in a magical forest and discovered an ancient secret.",
                    $"{name} and the {animal} set out on an exciting journey across mountains and rivers, finding treasures and making new friends."
                },
                ["mystery"] = new List<string>
                {
                    $"One foggy evening, {name} and their clever {animal} found a hidden key beneath an old tree. As they followed the clues, they uncovered a secret room.",
                    $"{name} and the {animal} solved riddles and followed mysterious footprints to reveal a long-lost treasure."
                },
                ["fantasy"] = new List<string>
                {
                    $"In a magical forest, {name} and a talking {animal} met a wise wizard who gave them a glowing crystal to save their village.",
                    $"{name} and the {animal} discovered a portal to a magical world filled with dragons and enchanted castles."
                },
                ["space"] = new List<string>
                {
                    $"Floating among the stars, {name} and their brave {animal} boarded a shiny spaceship to explore unknown planets and make new alien friends.",
                    $"{name} and the {animal} navigated through asteroid fields and discovered a new habitable planet."
                },
                ["nature"] = new List<string>
                {
                    $"One peaceful afternoon, {name} and their gentle {animal} wandered through blooming meadows and helped a lost baby bird find its way home.",
                    $"{name} and the {animal} explored forests and rivers, learning the secrets of nature and protecting wildlife."
                }
            };

            if (stories.ContainsKey(theme))
            {
                var variants = stories[theme];
                int index = rnd.Next(variants.Count); 
                return variants[index];
            }

            return "No stories were found for this topic, please choose another topic.";
        }
    }
}

