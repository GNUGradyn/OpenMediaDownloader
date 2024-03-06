using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenMediaDownloader
{
    public class YoutubeDlService : IYoutubeDlService
    {
        public async Task<Video> GetVideoMetadata(string url)
        {
            var exe = EmbeddedExeHelper.GetTempExePath("yt-dlp.exe");
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = $"-j \"{url}\"  --ffmpeg-location \"{EmbeddedExeHelper.TempFolder}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            // Log standard output
            string output = "";
            proc.OutputDataReceived += (sender, outline) =>
            {
                if (outline.Data != null) // Make sure to check for null to avoid appending null to the output string
                {
                    output += outline.Data;
                }
            };

            var tcs = new TaskCompletionSource<bool>();
            proc.EnableRaisingEvents = true;
            proc.Exited += (sender, args) =>
            {
                tcs.SetResult(true);
                proc.Dispose();
            };

            proc.Start();
            proc.BeginOutputReadLine();

            await tcs.Task; // This line effectively waits for the process to exit asynchronously
            return JsonConvert.DeserializeObject<Video>(output);
        }
        
        public event Action<float> DownloadProgressChanged;

        public void DownloadAndTrack(string url, string outputPath, string videoFormat, string audioFormat)
        {
            // Download logic...
            var exe = EmbeddedExeHelper.GetTempExePath("yt-dlp.exe");
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = $"-q --progress --newline -f \"${videoFormat}+{audioFormat}\" \"{url}\" --ffmpeg-location \"{EmbeddedExeHelper.TempFolder}\" -o \"{outputPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += (sender, outline) =>
            {
                if (outline.Data != null) 
                {
                    float progress = float.Parse(outline.Data.Split('%')[0]) / 100f;
                    DownloadProgressChanged?.Invoke(progress);
                }
            };
            proc.Exited += (sender, args) =>
            {
                proc.Dispose();
            };
            proc.Start();
            proc.BeginOutputReadLine();
        }
    }
}
