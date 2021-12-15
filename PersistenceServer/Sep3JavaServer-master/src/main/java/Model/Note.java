package Model;

public class Note {

    /**
     * Instance variables
     */
    private int id, groupId, week, year;
    private String name, status, text;

    /**
     * Constructor for Note
     * @param id
     * @param groupId
     * @param week
     * @param year
     * @param name
     * @param status
     * @param text
     */
    public Note(int id, int groupId, int week, int year, String name, String status, String text) {
        this.id = id;
        this.groupId = groupId;
        this.week = week;
        this.year = year;
        this.name = name;
        this.status = status;
        this.text = text;
    }

    /**
     * Get id
     * @return id
     */
    public int getId() {
        return id;
    }

    /**
     * Set id
     * @param id
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Get group id
     * @return groupId
     */
    public int getGroupId() {
        return groupId;
    }

    /**
     * Set group id
     * @param groupId
     */
    public void setGroupId(int groupId) {
        this.groupId = groupId;
    }

    /**
     * Get week
     * @return week
     */
    public int getWeek() {
        return week;
    }

    /**
     * Set week
     * @param week
     */
    public void setWeek(int week) {
        this.week = week;
    }

    /**
     * Get year
     * @return year
     */
    public int getYear() {
        return year;
    }

    /**
     * Set year
     * @param year
     */
    public void setYear(int year) {
        this.year = year;
    }

    /**
     * Get name
     * @return name
     */
    public String getName() {
        return name;
    }

    /**
     * Set name
     * @param name
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Get status
     * @return status
     */
    public String getStatus() {
        return status;
    }

    /**
     * Set status
     * @param status
     */
    public void setStatus(String status) {
        this.status = status;
    }

    /**
     * Get text
     * @return text
     */
    public String getText() {
        return text;
    }

    /**
     * Set text
     * @param text
     */
    public void setText(String text) {
        this.text = text;
    }
}
