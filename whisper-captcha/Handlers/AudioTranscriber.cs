namespace whisper_captcha.Handlers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class AudioTranscriber
{
    private readonly string _apiKey;
    private readonly string _apiUrl;

    public AudioTranscriber(string apiKey)
    {
        _apiKey = apiKey;
        _apiUrl = "https://api.openai.com/v1/audio/transcriptions";
    }

    public async Task<string> TranscribeAudioAsync(string filePath)
    {
        try
        {
            using var httpClient = new HttpClient();
            using var content = new MultipartFormDataContent();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var byteContent = new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(filePath));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("audio/mpeg");
            content.Add(byteContent, "file", "audio.mp3");

            content.Add(new StringContent("whisper-1"), "model");

            var response = await httpClient.PostAsync(_apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}