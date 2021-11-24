package Model;

public class GroupMembers {

    private int Id, UserId, GroupId;
    private String Username;

    public GroupMembers(int id, int userId, String username, int groupId ) {
        this.Id = id;
        this.UserId = userId;
        this.Username = username;
        this.GroupId = groupId;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        this.Id = id;
    }

    public int getUserId() {
        return UserId;
    }

    public void setUserId(int userId) {
        this.UserId = userId;
    }

    public int getGroupId() {
        return GroupId;
    }

    public void setGroupId(int groupId) {
        this.GroupId = groupId;
    }

    public String getUsername() {
        return Username;
    }

    public void setUsername(String username) {
        this.Username = username;
    }
}

