﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
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
        
        /// <summary>
        /// Progress, Processing, Done
        /// </summary>
        public event Action<int, bool, bool> DownloadProgressChanged;

        public void DownloadAndTrack(string url, string outputPath, string videoFormat, string audioFormat)
        {
            var exe = EmbeddedExeHelper.GetTempExePath("yt-dlp.exe");
            var formats = new List<string>();
            if (!string.IsNullOrEmpty(videoFormat)) formats.Add(videoFormat);
            if (!string.IsNullOrEmpty(audioFormat)) formats.Add(audioFormat);

            var arguments = $"-q --progress --progress-template \"%(progress._percent_str)s\" --no-color --remux-video {outputPath.Split('.').Last()} --newline -f \"{string.Join("+", formats)}\" \"{url}\" --ffmpeg-location \"{EmbeddedExeHelper.TempFolder}\" -o \"{outputPath}\"";

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += (sender, outline) =>
            {
                Console.WriteLine("[YTDL] " + outline.Data);
                if (outline.Data == null) return;
                float progress;
                if (float.TryParse(outline.Data.Replace(" ", "").Replace("%", ""), out progress))
                {
                    DownloadProgressChanged?.Invoke((int)Math.Round(progress), false, false);
                }
                else if (outline.Data.Replace(" ", "") == "NA")
                {
                    DownloadProgressChanged?.Invoke(100, true, false);
                }
                else
                {
                    var dialog = new Ookii.Dialogs.Wpf.TaskDialog()
                    {
                        WindowTitle = "Open Media Downloader",
                        Content = $"Download error - yt-dlp message not understood\n{outline.Data}",
                        MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Error
                    };
                    dialog.Buttons.Add(new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok));
                    dialog.Show();
                }
            };
            proc.ErrorDataReceived += (sender, outline) =>
            {
                Console.WriteLine("[YTDL] " + outline.Data);
                var dialog = new Ookii.Dialogs.Wpf.TaskDialog()
                {
                    WindowTitle = "Open Media Downloader",
                    Content = $"Download error\n{outline.Data}",
                    MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Error
                };
                dialog.Buttons.Add(new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok));
                dialog.Show();
            };
            proc.Exited += (sender, args) =>
            {
                if (proc.ExitCode != 0)
                {
                    var dialog = new Ookii.Dialogs.Wpf.TaskDialog()
                    {
                        WindowTitle = "Open Media Downloader",
                        Content = $"Non 0 ytdl exit code: " + proc.ExitCode,
                        MainIcon = Ookii.Dialogs.Wpf.TaskDialogIcon.Error
                    };
                    dialog.Buttons.Add(new Ookii.Dialogs.Wpf.TaskDialogButton(Ookii.Dialogs.Wpf.ButtonType.Ok));
                    dialog.Show();
                }
                DownloadProgressChanged?.Invoke(100, false, true);
                proc.Dispose();
            };
            Console.WriteLine("Invoking yt-dlp with args " + arguments);
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
        }
    }
}
