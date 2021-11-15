package PersistenceServer;

public class Invitation
{
  private int id;
  private int inviteeId;
  private int invitorId;

  public int getId()
  {
    return id;
  }

  public void setId(int id)
  {
    this.id = id;
  }

  public int getInviteeId()
  {
    return inviteeId;
  }

  public void setInviteeId(int inviteeId)
  {
    this.inviteeId = inviteeId;
  }

  public int getInvitorId()
  {
    return invitorId;
  }

  public void setInvitorId(int invitorId)
  {
    this.invitorId = invitorId;
  }

  public Invitation(int id, int inviteeId, int invitorId)
  {
    this.id = id;
    this.inviteeId = inviteeId;
    this.invitorId = invitorId;
  }
}
