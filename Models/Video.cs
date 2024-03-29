﻿using Newtonsoft.Json;
using OpenMediaDownloader.Models;

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
    public int Height { get; set; }
    [JsonProperty("duration")]
    public float Duration { get; set; }
    [JsonProperty("duration_string")]
    public string DurationString { get; set; }
    [JsonProperty("vcodec")]
    public string VideoCodec { get; set; }
    [JsonProperty("acodec")]
    public string AudioCodec { get; set; }
    [JsonProperty("fps")]
    public float? FPS { get; set; }
    [JsonProperty("ext")]
    public string Container { get; set; }
    [JsonProperty("formats")]
    public FormatOption[] Formats { get; set; }
}