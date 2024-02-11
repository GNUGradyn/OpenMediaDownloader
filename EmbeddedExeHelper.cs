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
        public static readonly Dictionary<string, string> TempExePaths;
        public static readonly string TempFolder;

        static EmbeddedExeHelper()
        {
            TempExePaths = new Dictionary<string, string>();
            // Use a fixed folder name within the temp directory
            TempFolder = Path.Combine(Path.GetTempPath(), "OpenMediaDownloaderExes");
            // Ensure the directory exists or create it
            Directory.CreateDirectory(TempFolder);

            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames().Where(name => name.Contains("OpenMediaDownloader.External") && name.EndsWith(".exe"));

            foreach (var resourceName in resourceNames)
            {
                var exeName = Path.GetFileNameWithoutExtension(resourceName).Split('.').Last() + ".exe"; // Extract the executable name
                var tempExePath = Path.Combine(TempFolder, exeName);
                Console.WriteLine($"Temporary path for {exeName}: {tempExePath}");

                // Check if the file already exists to avoid unnecessary extraction
                if (!File.Exists(tempExePath))
                {
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    using (var fileStream = File.Create(tempExePath))
                    {
                        stream?.CopyTo(fileStream);
                    }
                }

                TempExePaths[exeName] = tempExePath;
            }
        }

        public static string GetTempExePath(string exeName)
        {
            if (TempExePaths.TryGetValue(exeName, out var path))
            {
                return path;
            }
            else
            {
                throw new FileNotFoundException($"Executable {exeName} not found among embedded resources.");
            }
        }
    }
}
