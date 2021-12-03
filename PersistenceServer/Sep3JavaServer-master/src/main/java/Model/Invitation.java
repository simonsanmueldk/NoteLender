package Model;

public class Invitation {
    private int id;
    private int groupId;
    private String groupName;
    private int inviteeId;
    private String inviteeName;
    private int invitorId;
    private String invitorName;

    public Invitation(int id, int groupId, String groupName, int inviteeId, String inviteeName, int invitorId, String invitorName) {
        this.id = id;
        this.groupId = groupId;
        this.groupName = groupName;
        this.inviteeId = inviteeId;
        this.inviteeName = inviteeName;
        this.invitorId = invitorId;
        this.invitorName = invitorName;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getGroupId() {
        return groupId;
    }

    public void setGroupId(int groupId) {
        this.groupId = groupId;
    }

    public String getGroupName() {
        return groupName;
    }

    public void setGroupName(String groupName) {
        this.groupName = groupName;
    }

    public int getInviteeId() {
        return inviteeId;
    }

    public void setInviteeId(int inviteeId) {
        this.inviteeId = inviteeId;
    }

    public String getInviteeName() {
        return inviteeName;
    }

    public void setInviteeName(String inviteeName) {
        this.inviteeName = inviteeName;
    }

    public int getInvitorId() {
        return invitorId;
    }

    public void setInvitorId(int invitorId) {
        this.invitorId = invitorId;
    }

    public String getInvitorName() {
        return invitorName;
    }

    public void setInvitorName(String invitorName) {
        this.invitorName = invitorName;
    }
}
