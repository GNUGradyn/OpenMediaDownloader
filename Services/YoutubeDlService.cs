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
            var exe = EmbeddedExeHelper.TempExePath;
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = $"-j {url}",
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
    }
}
