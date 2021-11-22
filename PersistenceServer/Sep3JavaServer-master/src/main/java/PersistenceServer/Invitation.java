package PersistenceServer;

public class Invitation
{
  private int id;
  private int invitor_id;
  private int invitee_id;
  private int group_id;

  public int getId()
  {
    return id;
  }
  public void setId(int id)
  {
    this.id = id;
  }

  public int getInvitor_id()
  {
    return invitor_id;
  }

  public void setInvitor_id(int invitor_id)
  {
    this.invitor_id = invitor_id;
  }

  public int getInvitee_id()
  {
    return invitee_id;
  }

  public void setInvitee_id(int invitee_id)
  {
    this.invitee_id = invitee_id;
  }

  public int getGroup_id()
  {
    return group_id;
  }

  public void setGroup_id(int group_id)
  {
    this.group_id = group_id;
  }

  public Invitation(int id, int invitor_id, int invitee_id, int group_id)
  {
    this.id = id;
    this.invitor_id = invitor_id;
    this.invitee_id = invitee_id;
    this.group_id = group_id;
  }
}
