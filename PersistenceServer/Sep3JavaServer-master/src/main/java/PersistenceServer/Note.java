package PersistenceServer;

public class Note {

   private int id,user_id, group_id,week,year;
   private  String name, status, text;

    public Note(int id, int user_id, int group_id, int week, int year, String name, String status, String text) {
        this.id = id;
        this.user_id = user_id;
        this.group_id = group_id;
        this.week = week;
        this.year = year;
        this.name = name;
        this.status = status;
        this.text = text;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {this.id = id;}

    public int getUserId() {return user_id;}

    public void setUserId(int userId) {
        this.id = id;
    }

    public int getGroupId() {
        return group_id;
    }

    public void setGroupId(int groupId) {
        this.id = id;
    }

    public int getWeek() {
        return week;
    }

    public void setWeek(int week) {
        this.week = week;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}
