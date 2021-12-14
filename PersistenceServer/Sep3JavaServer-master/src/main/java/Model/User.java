package Model;

public class User {
    /**
     * Instance variables
     */
    private int id;
    private String firstName;
    private String lastName;
    private String username;
    private String password;

    /**
     * Constructor for User
     * @param id
     * @param firstName
     * @param lastName
     * @param username
     * @param password
     */
    public User(int id, String firstName, String lastName, String username, String password) {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.username = username;
        this.password = password;
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
     * Get first name
     * @return firstName
     */
    public String getFirstName() {
        return firstName;
    }

    /**
     * Set first name
     * @param firstName
     */
    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    /**
     * Get last name
     * @return lastName
     */
    public String getLastName() {
        return lastName;
    }

    /**
     * Set last name
     * @param lastName
     */
    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    /**
     * Get username
     * @return username
     */
    public String getUsername() {
        return username;
    }

    /**
     * Set username
     * @param username
     */
    public void setUsername(String username) {
        this.username = username;
    }

    /**
     * Get password
     * @return password
     */
    public String getPassword() {
        return password;
    }

    /**
     * Set password
     * @param password
     */
    public void setPassword(String password) {
        this.password = password;
    }
}
