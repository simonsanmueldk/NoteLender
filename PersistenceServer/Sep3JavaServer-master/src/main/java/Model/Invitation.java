package Model;

public class Invitation {
    /**
     * Instance variables
     */
    private int id;
    private int groupId;
    private String groupName;
    private int inviteeId;
    private String inviteeName;
    private int invitorId;
    private String invitorName;

    /**
     * Constructor for invitation
     * @param id
     * @param groupId
     * @param groupName
     * @param inviteeId
     * @param inviteeName
     * @param invitorId
     * @param invitorName
     */
    public Invitation(int id, int groupId, String groupName, int inviteeId, String inviteeName, int invitorId, String invitorName) {
        this.id = id;
        this.groupId = groupId;
        this.groupName = groupName;
        this.inviteeId = inviteeId;
        this.inviteeName = inviteeName;
        this.invitorId = invitorId;
        this.invitorName = invitorName;
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
     * Get group name
     * @return groupName
     */
    public String getGroupName() {
        return groupName;
    }

    /**
     * Sets group name
     * @param groupName
     */
    public void setGroupName(String groupName) {
        this.groupName = groupName;
    }

    /**
     * Get group id
     * @return inviteeId
     */
    public int getInviteeId() {
        return inviteeId;
    }

    /**
     * Set invitee id
     * @param inviteeId
     */
    public void setInviteeId(int inviteeId) {
        this.inviteeId = inviteeId;
    }

    /**
     * Get invitee name
     * @return inviteeName
     */
    public String getInviteeName() {
        return inviteeName;
    }

    /**
     * Set invitee name
     * @param inviteeName
     */
    public void setInviteeName(String inviteeName) {
        this.inviteeName = inviteeName;
    }

    /**
     * Get invitor id
     * @return invitorId
     */
    public int getInvitorId() {
        return invitorId;
    }

    /**
     * Set invitor id
     * @param invitorId
     */
    public void setInvitorId(int invitorId) {
        this.invitorId = invitorId;
    }

    /**
     * Get invitor name
     * @return invitorName
     */
    public String getInvitorName() {
        return invitorName;
    }

    /**
     * Set invitor name
     * @param invitorName
     */
    public void setInvitorName(String invitorName) {
        this.invitorName = invitorName;
    }
}
