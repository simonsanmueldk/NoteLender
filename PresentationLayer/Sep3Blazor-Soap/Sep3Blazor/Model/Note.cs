public class Note
{
    public int id{ get; set; }
    public int week{ get; set; }
    public int year{ get; set; }
    public string name{ get; set; }
    public string status{ get; set; }
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
    
    
}