using Newtonsoft.Json;

namespace OpenMediaDownloader.Models
{
    public class FormatOption
    {
        [JsonProperty("height")]
        public int? Height { get; set; }
        [JsonProperty("width")]
        public int? Width { get; set; }
        [JsonProperty("ext")]
        public string Container { get; set; }
        [JsonProperty("acodec")]
        public string AudioCodec { get; set; }
        [JsonProperty("vcodec")] 
        public string VideoCodec { get; set; }
        [JsonProperty("fps")]
        public float? FPS { get; set; }
    }
}