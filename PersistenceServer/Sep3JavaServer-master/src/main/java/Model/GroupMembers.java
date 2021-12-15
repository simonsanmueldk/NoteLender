package Model;

public class GroupMembers {

    /**
     * Instance variables
     */
    private int id, userId, groupId;
    private String username;

    /**
     * Constructor for GroupMembers
     * @param id
     * @param userId
     * @param username
     * @param groupId
     */
    public GroupMembers(int id, int userId, String username, int groupId) {
        this.id = id;
        this.userId = userId;
        this.username = username;
        this.groupId = groupId;
    }

    /**
     * Get id
     * @return id
     */
    public int getId() { return id; }

    /**
     * Set id
     * @param id
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Get user id
     * @return userId
     */
    public int getUserId() {
        return userId;
    }

    /**
     * Set user id
     * @param userId
     */
    public void setUserId(int userId) {
        this.userId = userId;
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
}

