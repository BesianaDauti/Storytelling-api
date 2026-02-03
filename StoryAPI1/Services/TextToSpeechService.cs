using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace StoryAPI1.Services
{
    public class TextToSpeechService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey;

        public TextToSpeechService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = configuration["GoogleTTS:ApiKey"];
        }

        public async Task<string> GenerateAudioAsync(string text)
        {
            var url = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={_apiKey}";

            var client = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                input = new { text = text },
                voice = new { languageCode = "en-US", name = "en-US-Wavenet-D" },
                audioConfig = new { audioEncoding = "MP3" }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseString);

            var audioContent = jsonDoc.RootElement.GetProperty("audioContent").GetString();

            return $"data:audio/mp3;base64,{audioContent}";
        }
    }
}
