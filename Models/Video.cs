using Newtonsoft.Json;

public class Video
{
    [JsonProperty("title")] 
    public string Title { get; set; }
    [JsonProperty("fulltitle")]
    public string FullTitle { get; set; }
    [JsonProperty("uploader")]
    public string Uploader { get; set; }
    [JsonProperty("thumbnail")]
    public string Thumbnail { get; set; }
    [JsonProperty("width")]
    public int Width { get; set; }
    [JsonProperty("height")]
    public int Hegiht { get; set; } 
}