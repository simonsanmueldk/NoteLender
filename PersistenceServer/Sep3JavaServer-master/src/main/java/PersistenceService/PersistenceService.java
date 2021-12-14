package PersistenceService;

import Model.*;
import com.google.gson.Gson;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

@Service public class PersistenceService implements IPersistenceService
{

  /**
   * Instance variables
   */
  private static final Gson gson = new Gson();
  private final String URL = "jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj";
  private final String USER = "seitjdhj";
  private final String PASSWORD = "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1";
  private Connection connection;

  /**
   * Constructor, throws exception if it is not connected.
   */
  public PersistenceService()
  {
    try
    {
      connection = DriverManager.getConnection(URL, USER, PASSWORD);
    }
    catch (SQLException throwables)
    {
      throwables.printStackTrace();
    }
  }

  /**
   * Edit note by updating the table notes in the database
   * @param note
   * @return
   */
  @Override public ResponseEntity<Void> editNote(String note)
  {

    Note temp = gson.fromJson(note, Note.class);
    String sqlStatement = "UPDATE notelender.notes SET week = ?, year = ?, name = ?, status = ?, text = ? WHERE id = ?";
    try
    {
      PreparedStatement editNote = connection.prepareStatement(sqlStatement);
      editNote.setInt(1, temp.getWeek());
      editNote.setInt(2, temp.getYear());
      editNote.setString(3, temp.getName());
      editNote.setString(4, temp.getStatus());
      editNote.setString(5, temp.getText());
      editNote.setInt(6, temp.getId());
      editNote.executeUpdate();

      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Add note by inserting a new note inside the table of notes in the database
   * @param note
   * @return
   */
  @Override public ResponseEntity<Void> addNote(String note)
  {
    Note temp = gson.fromJson(note, Note.class);
    String sqlStatement =
        "INSERT INTO notelender.notes (group_id,week,year,name,status,text) "
            + "VALUES (?,?,?,?,?,?)";
    try
    {
      PreparedStatement addNote = connection.prepareStatement(sqlStatement);
      addNote.setInt(1, temp.getGroupId());
      addNote.setInt(2, temp.getWeek());
      addNote.setInt(3, temp.getYear());
      addNote.setString(4, temp.getName());
      addNote.setString(5, temp.getStatus());
      addNote.setString(6, temp.getText());
      addNote.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method which takes notes from the list by group id
   * and stores them in NoteList
   * @param id
   * @return Notelist and HttpStatus.OK status, or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<List<Note>> getNoteList(int id)
  {
    List<Note> NoteList = new ArrayList<>();
    try
    {
      String sqlStatement = "SELECT * FROM notelender.notes WHERE group_id = ? ORDER BY year ASC, week";
      PreparedStatement getNote = connection.prepareStatement(sqlStatement);
      getNote.setInt(1, id);
      ResultSet rs = getNote.executeQuery();
      while (rs.next())
      {
        Note noteToAdd = new Note(rs.getInt(1), rs.getInt(2), rs.getInt(3),
            rs.getInt(4), rs.getString(5), rs.getString(6), rs.getString(7));
        NoteList.add(noteToAdd);
      }
      return new ResponseEntity<>(NoteList, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Add Group adds new group to both the table groups,
   * and groupmembers in database
   * @param name
   * @param memberId
   * @return HttpStatus.OK or  HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> postGroup(String name, int memberId)
  {
    String sqlStatement =
        "WITH groupcreation AS ( INSERT INTO notelender.groups (groupname) VALUES (?) "
            + "RETURNING id) INSERT INTO notelender.groupmembers (user_id, group_id)\n"
            + "VALUES ( ?, (SELECT id FROM groupcreation))";
    try
    {
      PreparedStatement insertGroup = connection.prepareStatement(sqlStatement);
      insertGroup.setString(1, name);
      insertGroup.setInt(2, memberId);
      insertGroup.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);

    }
    catch (Exception e)
    {
      e.printStackTrace();

      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method which takes groups from the table groupmembers and groups
   * and stores them in GroupList to retrieve them
   * @param id
   * @return GroupList, HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<List<Group>> getGroupList(int id)
  {
    List<Group> GroupList = new ArrayList<>();
    try
    {
      String sqlStatement =
          "SELECT g.id, groupname FROM notelender.groupmembers\n"
              + " INNER JOIN notelender.groups g ON g.id = groupmembers.group_id\n"
              + "WHERE user_id = ?";
      PreparedStatement getGroupMembersList = connection
          .prepareStatement(sqlStatement);
      getGroupMembersList.setInt(1, id);
      ResultSet rs = getGroupMembersList.executeQuery();
      while (rs.next())
      {
        GroupList.add(new Group(rs.getInt(1), rs.getString(2)));
      }
      return new ResponseEntity<>(GroupList, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Delete group from table groups in database.
   * @param group_id
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> deleteGroup(int group_id)
  {
    String sqlStatement = "DELETE FROM notelender.groups WHERE id= ?";
    try
    {
      PreparedStatement deleteGroup = connection.prepareStatement(sqlStatement);
      deleteGroup.setInt(1, group_id);
      deleteGroup.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method leaveGroup removes group_id and user_id from groupmembers table
   * in the database.
   * @param id
   * @param json
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> leaveGroup(int id, String json)
  {
    String sqlStatement = "DELETE FROM notelender.groupmembers WHERE group_id = ? AND user_id = ?";
    try
    {
      PreparedStatement deleteGroupMember = connection
          .prepareStatement(sqlStatement);
      deleteGroupMember.setInt(1, id);
      deleteGroupMember.setInt(2, Integer.parseInt(json));
      deleteGroupMember.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method which takes users from the table users
   * and stores them in userList to retrieve them
   * @param usersName
   * @return userList, HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<List<User>> getUserList(String usersName)
  {

    try
    {
      List<User> userList = new ArrayList<>();
      String sqlStatement = "SELECT * FROM notelender.users WHERE username LIKE ?";
      PreparedStatement getGroup = connection.prepareStatement(sqlStatement);
      getGroup.setString(1, "%" + usersName + "%");
      ResultSet rs = getGroup.executeQuery();
      while (rs.next())
      {
        userList.add(new User(rs.getInt(1), rs.getString(2), rs.getString(3),
            rs.getString(4), null));
      }
      return new ResponseEntity<>(userList, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }

  }

  /**
   * Method validates user by checking username from useres table in database
   * @param user
   * @return validatedUser, HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<User> validateUser(String user)
  {
    User validatedUser = null;
    User temp = gson.fromJson(user, User.class);
    String sqlStatement = "SELECT * FROM notelender.users WHERE username =  ?";
    try
    {
      PreparedStatement validateUser = connection
          .prepareStatement(sqlStatement);
      validateUser.setString(1, temp.getUsername());
      ResultSet rs = validateUser.executeQuery();
      while (rs.next())
      {
        validatedUser = new User(rs.getInt(1), rs.getString(2), rs.getString(3),
            rs.getString(4), rs.getString(5));
      }
      return new ResponseEntity<>(validatedUser, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method adds new user in useres table in database
   * @param user
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> registerUser(String user)
  {
    User temp = gson.fromJson(user, User.class);
    String sqlStatement = "INSERT INTO notelender.users (firstname,lastname,username,password) VALUES (?,?,?,?)";
    try
    {
      PreparedStatement registerUser = connection
          .prepareStatement(sqlStatement);
      registerUser.setString(1, temp.getFirstName());
      registerUser.setString(2, temp.getLastName());
      registerUser.setString(3, temp.getUsername());
      registerUser.setString(4, temp.getPassword());
      registerUser.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method can edit user by updating password in users table in database
   * @param user
   * @param user_id
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> editUser(String user, int user_id)
  {
    User temp = gson.fromJson(user, User.class);
    String sqlStatement = "UPDATE notelender.users SET password= ? WHERE id= ?";
    try
    {
      PreparedStatement editUser = connection.prepareStatement(sqlStatement);
      editUser.setString(1, temp.getPassword());
      editUser.setInt(2, user_id);
      editUser.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Removes user from useres table in database using id
   * @param userId
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> deleteUser(int userId)
  {
    String sqlStatement = "DELETE FROM notelender.users WHERE id = ?";
    try
    {
      PreparedStatement statement = connection.prepareStatement(sqlStatement);
      statement.setInt(1, userId);
      statement.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method which gets from the table groupmembers
   * and stores them in GroupMembersList to retrieve them
   * @param id
   * @return GroupMembersList, HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<List<GroupMembers>> getGroupMemberList(int id)
  {
    List<GroupMembers> GroupMembersList = new ArrayList<>();
    try
    {
      String sqlStatement =
          "SELECT notelender.groupmembers.id,user_id,u.username,group_id "
              + "FROM notelender.groupmembers INNER JOIN notelender.users u ON u.id = groupmembers.user_id "
              + "WHERE notelender.groupmembers.group_id = ?";
      PreparedStatement getUserList = connection.prepareStatement(sqlStatement);
      getUserList.setInt(1, id);
      ResultSet rs = getUserList.executeQuery();
      while (rs.next())
      {
        GroupMembers groupMembers = new GroupMembers(rs.getInt(1), rs.getInt(2),
            rs.getString(3), rs.getInt(4));
        GroupMembersList.add(groupMembers);
      }
      return new ResponseEntity<>(GroupMembersList, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method adds new groupmember to groupmembers table in database
   * @param groupMember
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> addGroupMember(String groupMember)
  {
    String sqlStatement = "INSERT INTO notelender.groupmembers (user_id,group_id) VALUES (?,?)";
    GroupMembers groupMembers = gson.fromJson(groupMember, GroupMembers.class);
    try
    {
      PreparedStatement addGroupMember = connection
          .prepareStatement(sqlStatement);
      addGroupMember.setInt(1, groupMembers.getUserId());
      addGroupMember.setInt(2, groupMembers.getGroupId());
      addGroupMember.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Removes group members from groupmembers table in database using id
   * @param id
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> deleteGroupMember(int id)
  {
    String sqlStatement = "DELETE FROM notelender.groupmembers WHERE id=?";
    try
    {
      PreparedStatement statement = connection.prepareStatement(sqlStatement);
      statement.setInt(1, id);
      statement.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Removes note from notes table in database using id
   * @param noteId
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> deleteNote(int noteId)
  {
    String sqlStatement = "DELETE FROM notelender.notes WHERE id = ?";
    try
    {
      PreparedStatement statement = connection.prepareStatement(sqlStatement);
      statement.setInt(1, noteId);
      statement.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Inserts new invitation in invitations table in database
   * @param invitation
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> addInvitation(String invitation)
  {
    {

      Invitation temp = gson.fromJson(invitation, Invitation.class);
      String sqlStatement = "INSERT INTO notelender.invitations (invitor_id,invitee_id,group_id) VALUES (?,?,?)";
      try
      {
        PreparedStatement addInvitation = connection
            .prepareStatement(sqlStatement);
        addInvitation.setInt(1, temp.getInvitorId());
        addInvitation.setInt(2, temp.getInviteeId());
        addInvitation.setInt(3, temp.getGroupId());
        addInvitation.executeUpdate();
        return new ResponseEntity<>(HttpStatus.OK);
      }
      catch (Exception e)
      {
        e.printStackTrace();
        return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
      }
    }
  }

  /**
   * Removes invitation from invitations table in database
   * @param id
   * @return HttpStatus.OK or HttpStatus.BAD_REQUEST
   */
  @Override public ResponseEntity<Void> deleteInvitation(int id)
  {
    String sqlStatement = "DELETE FROM notelender.invitations WHERE id= ?";
    try
    {
      PreparedStatement deleteInvitation = connection
          .prepareStatement(sqlStatement);
      deleteInvitation.setInt(1, id);
      deleteInvitation.executeUpdate();
      return new ResponseEntity<>(HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }

  /**
   * Method which gets from the table groupmembers
   * and stores them in InvitationList to retrieve the invitations
   * @param id
   * @return
   */
  @Override public ResponseEntity<List<Invitation>> getInvitationList(int id)
  {
    List<Invitation> InvitationList = new ArrayList<>();
    try
    {
      String sqlStatement =
          "SELECT invitations.id, g.id, g.groupname, invitations.invitee_id, u2.username, "
              + "invitations.invitor_id, u.username FROM notelender.invitations\n"
              + "         INNER JOIN notelender.groups g ON g.id = invitations.group_id\n"
              + "         INNER JOIN notelender.users u ON u.id = invitations.invitor_id\n"
              + "         INNER JOIN notelender.users u2 ON u2.id = invitations.invitee_id\n"
              + "WHERE invitee_id = ?";
      PreparedStatement getInvitation = connection
          .prepareStatement(sqlStatement);
      getInvitation.setInt(1, id);
      ResultSet rs = getInvitation.executeQuery();
      while (rs.next())
      {
        InvitationList.add(
            new Invitation(rs.getInt(1), rs.getInt(2), rs.getString(3),
                rs.getInt(4), rs.getString(5), rs.getInt(6), rs.getString(7)));
      }
      return new ResponseEntity<>(InvitationList, HttpStatus.OK);
    }
    catch (Exception e)
    {
      e.printStackTrace();
      return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
  }
}


