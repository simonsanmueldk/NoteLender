package PersistenceServer;

public class Invitation
{
  private int id;
  private int invitorId;
  private int inviteeId;
  private int groupId;

  public int getId()
  {
    return id;
  }
  public void setId(int id)
  {
    this.id = id;
  }

  public int getInvitorId()
  {
    return invitorId;
  }

  public void setInvitorId(int invitorId)
  {
    this.invitorId = invitorId;
  }

  public int getInviteeId()
  {
    return inviteeId;
  }

  public void setInviteeId(int inviteeId)
  {
    this.inviteeId = inviteeId;
  }

  public int getGroupId()
  {
    return groupId;
  }

  public void setGroupId(int groupId)
  {
    this.groupId = groupId;
  }

  public Invitation(int id, int invitorId, int inviteeId, int groupId)
  {
    this.id = id;
    this.groupId = groupId;
    this.inviteeId = inviteeId;
    this.invitorId = invitorId;

  }
}
