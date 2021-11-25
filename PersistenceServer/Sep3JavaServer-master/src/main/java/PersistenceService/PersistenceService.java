package PersistenceService;

import Model.*;
import com.google.gson.Gson;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PersistenceService implements IPersistenceService {

    private static final Gson gson = new Gson();
    private final String URL = "jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj";
    private final String USER = "seitjdhj";
    private final String PASSWORD = "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1";
    private final Connection connection = DriverManager.getConnection(URL, USER, PASSWORD);

    public PersistenceService() throws SQLException {
    }

    @Override
    public String postGroup(String json) {
        String insertString = "INSERT INTO notelender.groups (groupname) VALUES (?)";
        List<Group> GroupList = new ArrayList<>();
        try {
            PreparedStatement insertGroup = connection.prepareStatement(insertString, PreparedStatement.RETURN_GENERATED_KEYS);
            insertGroup.setString(1, json);
            insertGroup.executeUpdate();
            try (ResultSet generatedKeys = insertGroup.getGeneratedKeys()) {
                if (generatedKeys.next()) {
                    Group groupToAdd = new Group(generatedKeys.getInt(1), generatedKeys.getString(2));
                    GroupList.add(groupToAdd);
                    return gson.toJson(GroupList);
                } else {
                    throw new SQLException("Creating failed, no ID obtained.");
                }
            }
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public String addNote(String json) {
        String addString =
                "INSERT INTO notelender.notes (id,user_id,group_id,week,year,name,status,text) VALUES (?,?,?,?,?,?,?,?)";
        System.out.println("JSON: " + json);
        Note note = gson.fromJson(json, Note.class);
        System.out.println("Note: " + note.getId());
        try {
            PreparedStatement addNote = connection.prepareStatement(addString, PreparedStatement.RETURN_GENERATED_KEYS);
            addNote.setInt(1, note.getId());
            addNote.setInt(2, note.getUserId());
            addNote.setInt(3, note.getGroupId());
            addNote.setInt(4, note.getWeek());
            addNote.setInt(5, note.getYear());
            addNote.setString(6, note.getName());
            addNote.setString(7, note.getStatus());
            addNote.setString(8, note.getText());
            addNote.executeUpdate();
            try (ResultSet keys = addNote.getGeneratedKeys()) {
                if (keys.next()) {
                    Note noteToAdd = new Note(keys.getInt(1), keys.getInt(2),
                            keys.getInt(3), keys.getInt(4),
                            keys.getInt(5), keys.getString(6),
                            keys.getString(7), keys.getString(8));
                    return gson.toJson(noteToAdd);
                } else {
                    throw new SQLException("Creating failed, no ID obtained.");
                }
            }
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public String deleteGroup(int id) throws SQLException {
        String deleteString = "DELETE FROM notelender.groups WHERE id= ?";
        PreparedStatement deleteGroup = connection.prepareStatement(deleteString);
        deleteGroup.setInt(1, id);
        int deleted = deleteGroup.executeUpdate();
        if (deleted == 0) {
            return "Fail";
        } else {
            return "Success";
        }
    }

    @Override
    public String getNote(int id) throws SQLException {
        List<Note> NoteList = new ArrayList<>();

        String getString = "SELECT * FROM notelender.notes WHERE group_id = ? ORDER BY year ASC, week";
        PreparedStatement getNote = connection.prepareStatement(getString);
        getNote.setInt(1, id);
        ResultSet rs = getNote.executeQuery();

        while (rs.next()) {
            Note noteToAdd = new Note(rs.getInt(1), rs.getInt(2),
                    rs.getInt(3), rs.getInt(4),
                    rs.getInt(5), rs.getString(6),
                    rs.getString(7), rs.getString(8));
            NoteList.add(noteToAdd);
        }
        return gson.toJson(NoteList);
    }

    @Override
    public String getGroup(int id) throws SQLException {
        List<Group> GroupList = new ArrayList<>();
        String getString = "SELECT * FROM notelender.groups WHERE id =  ?";
        PreparedStatement getGroup = connection.prepareStatement(getString);
        getGroup.setInt(1, id);
        ResultSet rs = getGroup.executeQuery();

        while (rs.next()) {
            Group groupToAdd = new Group(rs.getInt(1), rs.getString(2));
            GroupList.add(groupToAdd);
        }
        return gson.toJson(GroupList);
    }

    @Override
    public String getUserList(int id) throws SQLException {
        List<GroupMembers> GroupMembersList = new ArrayList<>();
        System.out.println(id);
        String getString = "SELECT notelender.groupmembers.id,user_id,u.username,group_id from notelender.groupmembers INNER JOIN notelender.users u on u.id = groupmembers.user_id where notelender.groupmembers.group_id = ?";
        PreparedStatement getUserList = connection.prepareStatement(getString);
        getUserList.setInt(1, id);
        ResultSet rs = getUserList.executeQuery();
        while (rs.next()) {
            GroupMembers groupMembers = new GroupMembers(rs.getInt(1), rs.getInt(2), rs.getString(3), rs.getInt(4));
            GroupMembersList.add(groupMembers);
            System.out.println(groupMembers.getUsername());
        }
        return gson.toJson(GroupMembersList);
    }

    /*
        Note section
     */

    @Override
    public String validateUser(String json) {
        User user = null;
        User temp = gson.fromJson(json, User.class);
        String getString = "SELECT * FROM notelender.users WHERE username =  ?";
        try {
            PreparedStatement validateUser = connection.prepareStatement(getString);
            validateUser.setString(1, temp.getUsername());
            ResultSet rs = validateUser.executeQuery();
            while (rs.next()) {
                user = new User(rs.getInt(1), rs.getString(2),
                        rs.getString(3), rs.getString(4),
                        rs.getString(5));
            }
            System.out.println("Login is working");
            return gson.toJson(user);
        } catch (Exception e) {
            return null;
        }

    }

    @Override
    public String registerUser(String json) {
        User temp = gson.fromJson(json, User.class);
        String registerString = "INSERT INTO notelender.users (firstname,lastname,username,password) VALUES (?,?,?,?)";
        try {
            PreparedStatement registerUser = connection.prepareStatement(registerString, PreparedStatement.RETURN_GENERATED_KEYS);
            registerUser.setString(1, temp.getFirstName());
            registerUser.setString(2, temp.getLastName());
            registerUser.setString(3, temp.getUsername());
            registerUser.setString(4, temp.getPassword());
            registerUser.executeUpdate();

            try (ResultSet keys = registerUser.getGeneratedKeys()) {
                if (keys.next()) {
                    User user = new User(keys.getInt(1), keys.getString(2),
                            keys.getString(3), keys.getString(4), keys.getString(5));
                    System.out.println("register User is working");
                    return gson.toJson(user);
                } else {
                    throw new SQLException("Creating failed, no ID obtained.");
                }
            }
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
        //     statement.executeUpdate("INSERT INTO notelender.users (firstname,lastname,username,password)
        //     VALUES ('" + temp.getFirstName() + "','" + temp.getLastName() + "','" + temp.getUsername()
        //     + "','" + temp.getPassword() + "')", Statement.RETURN_GENERATED_KEYS);
    }

    @Override
    public String editUser(String json, int user_id) {
        User temp = gson.fromJson(json, User.class);
        String editString = "UPDATE notelender.users SET password= ? + WHERE id= ?";
        try {
            PreparedStatement editUser = connection.prepareStatement(editString, PreparedStatement.RETURN_GENERATED_KEYS);
            editUser.setString(1, temp.getPassword());
            editUser.setInt(2, temp.getId());
            editUser.executeUpdate();

            try (ResultSet keys = editUser.getGeneratedKeys()) {
                if (keys.next()) {
                    User user = new User(keys.getInt(1), keys.getString(2),
                            keys.getString(3), keys.getString(4),
                            keys.getString(5));
                    System.out.println("edit User is working");
                    return gson.toJson(user);
                } else {
                    throw new SQLException("Creating failed, no ID obtained.");
                }
            }
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;

    }

    @Override
    public String deleteNote(int noteId){
        String sql = "DELETE FROM notelender.notes WHERE id = ?";

        try{
            PreparedStatement statement = connection.prepareStatement(sql, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, noteId);
            statement.executeUpdate();

        } catch (SQLException sqlException) {
            sqlException.printStackTrace();
        }
        return null;
    }

    @Override
    public String deleteUser(int userId) {
        String sql = "DELETE FROM notelender.user WHERE id = ?";

        try{
            PreparedStatement statement = connection.prepareStatement(sql, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, userId);
            statement.executeUpdate();

        } catch (SQLException sqlException) {
            sqlException.printStackTrace();
        }
        return null;
    }

    @Override
    public String addInvitation(String json) {
        {
            System.out.println("JSON: " + json);
            Invitation invitation = gson.fromJson(json, Invitation.class);

            String addString = "INSERT INTO notelender.invitations (id,invitor_id,invitee_id,group_id) VALUES (?,?,?,?)";
            try {
                PreparedStatement addInvitation = connection.prepareStatement(addString, PreparedStatement.RETURN_GENERATED_KEYS);
                addInvitation.setInt(1, invitation.getId());
                addInvitation.setInt(2, invitation.getInvitorId());
                addInvitation.setInt(3, invitation.getInviteeId());
                addInvitation.setInt(4, invitation.getGroupId());
                addInvitation.executeUpdate();

                try (ResultSet keys = addInvitation.getGeneratedKeys()) {
                    if (keys.next()) {
                        Invitation invitationToAdd = new Invitation(keys.getInt(1), keys.getInt(2),
                                keys.getInt(3), keys.getInt(4));
                        return gson.toJson(invitationToAdd);
                    } else {
                        throw new SQLException("Creating failed, no ID obtained.");
                    }
                }
            } catch (SQLException e) {
                System.out.println("Connection failure.");
                e.printStackTrace();
            }
            return null;
        }

    }


    @Override
    public String getInvitationList(String id)
            throws SQLException {

        System.out.println("Get invitation man");
        List<Invitation> InvitationList = new ArrayList<>();
        String getString = "SELECT * FROM notelender.invitations WHERE id = ?";
        PreparedStatement getInvitation = connection.prepareStatement(getString);
        getInvitation.setInt(1, Integer.valueOf(id));
        ResultSet rs = getInvitation.executeQuery();
        while (rs.next()) {
            Invitation invitationToAdd = new Invitation(rs.getInt(1),
                rs.getInt(2),rs.getInt(3),rs.getInt(4));
            InvitationList.add(invitationToAdd);
        }
        return gson.toJson(InvitationList);
    }

    @Override public String deleteInvitation(String id) throws SQLException
    {
        String deleteString = "DELETE FROM notelender.invitations WHERE id= ?";
        PreparedStatement deleteInvitation = connection.prepareStatement(deleteString);
        deleteInvitation.setInt(1, Integer.valueOf(id));
        int deleted = deleteInvitation.executeUpdate();
        ;
        if (deleted == 0) {
            return "Fail";
        } else {
            return "Success";
        }
    }

}


