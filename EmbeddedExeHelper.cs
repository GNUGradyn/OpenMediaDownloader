using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OpenMediaDownloader
{
    public static class EmbeddedExeHelper
    {
        public static readonly string TempExePath;

        static EmbeddedExeHelper()
        {
            // Logic to extract the EXE and set `TempExePath`
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "OpenMediaDownloader.yt-dlp.exe";
            var tempFile = Path.GetTempFileName();
            File.Delete(tempFile); // Delete the temp file created by GetTempFileName
            TempExePath = Path.ChangeExtension(tempFile, ".exe");
            Console.WriteLine("Temporary path for yt-dlp: " + TempExePath);

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var fileStream = File.Create(TempExePath))
            {
                stream?.CopyTo(fileStream);
            }
        }
    }
}
