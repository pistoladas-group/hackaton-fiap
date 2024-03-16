using System.Drawing;
using System.IO.Compression;
using FFMpegCore;
using Hackaton.Shared.MessageBus;
using Hackaton.Shared.Messages.Events;
using Hackaton.Worker.Configurations;
using Serilog;

namespace Hackaton.Worker;

public class Worker(IMessageBus bus) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("Worker has started");
        // bus.Consume<NewVideoEvent>(EnvironmentVariables.BrokerNewVideoToProcessQueueName, ExecuteAfterConsumed);
        await ExecuteAfterConsumed(new NewVideoEvent(Guid.NewGuid()));
    }
    
    private async Task ExecuteAfterConsumed(NewVideoEvent? message)
    {
        Log.Information("New message received: {@message}", message);

        if (message is null)
        {
            Log.Warning("Message is null. Skipping e-mail notification");
            return;
        }

        try
        {
            Log.Information("Processing started");
            var videoPath = @"C:\temp\video.mp4";

            //TODO: Download video to Path
            var videoMemoryStream = await GetVideoFileMemoryStream(message.VideoId);
            
            //TODO: 

            var imagesOutputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Images");

            if (!Directory.Exists(imagesOutputFolder))
            {
                Log.Information("Creating Images directory");
                Directory.CreateDirectory(imagesOutputFolder);
            }

            var videoInfo = await FFProbe.AnalyseAsync(stream: videoMemoryStream);
            var duration = videoInfo.Duration;

            var interval = TimeSpan.FromSeconds(20);

            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                Log.Information($"Processing frame: {currentTime}");

                var outputPath = Path.Combine(imagesOutputFolder, $"frame_at_{currentTime.TotalSeconds}.jpg");
                await FFMpeg.SnapshotAsync(videoPath, outputPath, new Size(1920, 1080), currentTime);
            }
            
            //TODO: process zip

            var zipFileOutputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Zip");
            
            if (!Directory.Exists(zipFileOutputFolder))
            {
                Log.Information("Creating Zip files directory");
                Directory.CreateDirectory(zipFileOutputFolder);
            }

            ZipFile.CreateFromDirectory(imagesOutputFolder, zipFileOutputFolder);

            Log.Information("Video processing finalized");
        }
        catch (Exception e)
        {
            Log.Error(e, "Error while sending notification");
            throw;
        }
    }

    private async Task<MemoryStream> GetVideoFileMemoryStream(Guid fileId)
    {
        // try
        // {
        //     var videoOutputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Video");
        //
        //     var client = new HttpClient();
        //     
        //     Log.Information("Downloading file");
        //     var fileRequest = await client.GetStreamAsync(_fileUrl);
        //     
        //     var memoryStream = new MemoryStream();
        //     await fileRequest.CopyToAsync(memoryStream);
        //
        //     await using var file = new FileStream($"{fileId}.mp4", FileMode.Create, FileAccess.Write);
        //     byte[] bytes = new byte[ms.Length];
        //     ms.Read(bytes, 0, (int)ms.Length);
        //     file.Write(bytes, 0, bytes.Length);
        //     ms.Close();
        //
        //
        //     return memoryStream;
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine(e);
        //     throw;
        // }
        
        var videoPath = @"C:\temp\video.mp4";
        var videoFileBytes = File.ReadAllBytes(videoPath);
        var videoFileStream = new MemoryStream(videoFileBytes);

        return videoFileStream;
    }
}
