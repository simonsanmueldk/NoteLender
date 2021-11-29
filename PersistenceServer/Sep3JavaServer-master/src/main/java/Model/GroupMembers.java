package Model;

public class GroupMembers {

    private int id, userId, groupId;
    private String username;

    public GroupMembers(int id, int userId, String username, int groupId ) {
        this.id = id;
        this.userId = userId;
        this.username = username;
        this.groupId = groupId;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public int getGroupId() {
        return groupId;
    }

    public void setGroupId(int groupId) {
        this.groupId = groupId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }
}

