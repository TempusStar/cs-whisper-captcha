using System;
using System.Threading.Tasks;
using whisper_captcha.Handlers;


class Program
{
    static async Task Main(string[] args)
    {
        var filePath = "cap-dl/audio.mp3";
        var apiKey = "OPENAI_KEY"; // Use your actual OpenAI API key

        var transcriber = new AudioTranscriber(apiKey);
        var transcription = await transcriber.TranscribeAudioAsync(filePath);

        if (transcription != null)
        {
            Console.WriteLine(transcription);
        }
        else
        {
            Console.WriteLine("Failed to transcribe audio.");
        }
    }
}