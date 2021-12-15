package Model;

public class Group {

    /**
     * Instance variables
     */
    private int id;
    private String name;

    /**
     * Constructor for group
     * @param id
     * @param name
     */
    public Group(int id, String name) {
        this.id = id;
        this.name = name;
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
}
