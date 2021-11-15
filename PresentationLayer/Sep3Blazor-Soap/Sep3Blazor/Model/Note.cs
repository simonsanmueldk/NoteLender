using System;
using System.Text.Json.Serialization;


public class Note 

{
    [JsonPropertyName("id")]
    public int id{ get; set; }
    [JsonPropertyName("week")]
    public int week{ get; set; }
    [JsonPropertyName("year")]
    public int year{ get; set; }
    [JsonPropertyName("name")]
    public string name{ get; set; }
    [JsonPropertyName("status")]
    public string status{ get; set; }
    [JsonPropertyName("text")]
    public string text{ get; set; }

    public Note(int id, int week, int year, string name, string status, string text)
    {
        this.id = id;
        this.week = week;
        this.year = year;
        this.name = name;
        this.status = status;
        this.text = text;
    }

    public Note(int week, int year, string name, string status, string text)
    {
        this.week = week;
        this.year = year;
        this.name = name;
        this.status = status;
        this.text = text;
    }
}