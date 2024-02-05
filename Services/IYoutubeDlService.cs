using System.Threading.Tasks;

public interface IYoutubeDlService
{
    Task<Video> GetVideoMetadata(string url);
}