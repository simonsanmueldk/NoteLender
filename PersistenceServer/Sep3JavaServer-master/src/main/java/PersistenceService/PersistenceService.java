package PersistenceService;

import Model.Group;
import Model.Invitation;
import Model.Note;
import Model.User;
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
        String insertString =
                "INSERT INTO notelender.groups (groupname) VALUES (?)";
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
    public String addNote(String json) throws SQLException {
        System.out.println("JSON: " + json);
        Note note = gson.fromJson(json, Note.class);
        System.out.println("Note: " + note.getId());

        try {
            Statement statement = connection.createStatement();
            statement.executeUpdate("INSERT INTO notelender.notes (id,user_id,group_id,week,year,name,status,text) VALUES ("
                    + note.getId() + "," + note.getUserId() + ","
                    + note.getGroupId() + "," + note.getWeek() + ","
                    + note.getYear() + ",'" + note.getName() + "','"
                    + note.getStatus() + "','" + note.getText() + "')", Statement.RETURN_GENERATED_KEYS);
            try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
                if (generatedKeys.next()) {
                    Note noteToAdd = new Note(generatedKeys.getInt(1), generatedKeys.getInt(2),
                            generatedKeys.getInt(3), generatedKeys.getInt(4),
                            generatedKeys.getInt(5), generatedKeys.getString(6),
                            generatedKeys.getString(7), generatedKeys.getString(8));
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
        ;
        if (deleted == 0) {
            return "Fail";
        } else {
            return "Success";
        }
//        PreparedStatement statement = connection.prepareStatement
//                ("DELETE FROM notelender.groups WHERE id='" + id + "'");
    }

    @Override
    public String getNote(int id) throws SQLException {
        List<Note> NoteList = new ArrayList<>();

        String getString = "SELECT * FROM notelender.notes WHERE group_id = ?";
        PreparedStatement getNote = connection.prepareStatement(getString);
        getNote.setInt(1, id);
        ResultSet rs = getNote.executeQuery();
//
        while (rs.next()) {
            Note noteToAdd = new Note(rs.getInt(1), rs.getInt(2),
                    rs.getInt(3), rs.getInt(4),
                    rs.getInt(5), rs.getString(6),
                    rs.getString(7), rs.getString(8));
            NoteList.add(noteToAdd);
        }
        return gson.toJson(NoteList);

//        ResultSet resultSet = connection.createStatement().executeQuery
//                ("SELECT * FROM notelender.notes WHERE group_id = " + id);
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

//        ResultSet resultSet = connection.createStatement().executeQuery
//                ("SELECT * FROM notelender.groups WHERE id = " + id);
    }

    @Override
    public String getUserList(int id) throws SQLException {
        String text = "";
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM sep3.notes WHERE id = " + id);
        while (resultSet.next()) {
            text = resultSet.getString(2);
            System.out.println(text);
        }
        List<String> AdultsList = new ArrayList<>();
        AdultsList.add(text);
        return gson.toJson(AdultsList);
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
//        ResultSet rs = connection.createStatement().executeQuery
//                ("SELECT * FROM notelender.users WHERE username ='" + temp.getUsername() + "'");
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

            try (ResultSet gk = registerUser.getGeneratedKeys()) {
                if (gk.next()) {
                    User user = new User(gk.getInt(1), gk.getString(2),
                            gk.getString(3), gk.getString(4), gk.getString(5));
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
    public String editUser(String json, int user_id) throws SQLException {
        User temp = gson.fromJson(json, User.class);
        User user;
        try {
            Statement statement = connection.createStatement();
            statement.executeUpdate("UPDATE notelender.users SET password='" + temp.getPassword() + "'WHERE id=" + temp.getId() + "", Statement.RETURN_GENERATED_KEYS);
            try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
                if (generatedKeys.next()) {
                    user = new User(generatedKeys.getInt(1), generatedKeys.getString(2), generatedKeys.getString(3), generatedKeys.getString(4), generatedKeys.getString(5));
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
    public String addInvitation(String json) {
        {
            System.out.println("JSON: " + json);
            Invitation invitation = gson.fromJson(json, Invitation.class);
            System.out.println("ID: " + invitation.getId());
            System.out.println("InvitorId: " + invitation.getInvitorId());
            System.out.println("InviteeId: " + invitation.getInviteeId());
            System.out.println("GroupId: " + invitation.getGroupId());

            try {
                Statement statement = connection.createStatement();
                statement.executeUpdate("INSERT INTO notelender.invitations (id,invitor_id,invitee_id,group_id) VALUES ("
                        + invitation.getId() + "," + invitation.getGroupId() + ","
                        + invitation.getInviteeId() + "," + invitation.getInvitorId() + ")", Statement.RETURN_GENERATED_KEYS);
                try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
                    if (generatedKeys.next()) {
                        Invitation invitationToAdd = new Invitation(generatedKeys.getInt(1),generatedKeys.getInt(2),generatedKeys.getInt(3),generatedKeys.getInt(4));
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
    public String getInvitation(int id)
            throws SQLException {
        String text = "";
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM sep3.notelendar.invitations WHERE id = " + id);
        while (resultSet.next()) {
            text = resultSet.getString(2);
            System.out.println(text);
        }
        List<String> invitationsList = new ArrayList<>();
        invitationsList.add(text);
        return gson.toJson(invitationsList);

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


}


