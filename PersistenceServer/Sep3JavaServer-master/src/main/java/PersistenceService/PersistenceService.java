package PersistenceService;

import PersistenceServer.Group;
import PersistenceServer.Invitation;
import PersistenceServer.Note;
import PersistenceServer.User;
import com.google.gson.Gson;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PersistenceService implements IPersistenceService {

    private static final Gson gson = new Gson();
    private final Connection connection =
            DriverManager.getConnection("jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj",
                    "seitjdhj", "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1");


    public PersistenceService() throws SQLException {
    }

    @Override
    public String getGroup(int id) throws SQLException {
        List<Group> GroupList = new ArrayList<>();
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.groups WHERE id = " + id);
        while (resultSet.next()) {
            Group groupToAdd = new Group(resultSet.getInt(1), resultSet.getString(2));
            GroupList.add(groupToAdd);
        }
        return gson.toJson(GroupList);
    }

    @Override
    public String createGroup(String json) throws SQLException {
        List<Group> GroupList = new ArrayList<>();
        try {
            Statement statement = connection.createStatement();
            statement.executeUpdate("INSERT INTO notelender.groups (name) VALUES ('" + json + "')", Statement.RETURN_GENERATED_KEYS);
            try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
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
    public String deleteGroup(int id) throws SQLException {
        PreparedStatement statement = connection.prepareStatement
                ("DELETE FROM notelender.groups WHERE id='" + id + "'");
        int deleted = statement.executeUpdate();
        if (deleted == 0) {
            return "Fail";
        } else {
            return "Success";
        }
    }

    @Override
    public String getNoteList(int id) throws SQLException {
        List<Note> NoteList = new ArrayList<>();
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.note WHERE group_id = " + id);
        while (resultSet.next()) {
            Note noteToAdd = new Note(resultSet.getInt(1), resultSet.getInt(2),
                    resultSet.getInt(3), resultSet.getInt(4),
                    resultSet.getInt(5), resultSet.getString(6),
                    resultSet.getString(7), resultSet.getString(8));
            NoteList.add(noteToAdd);
        }
        return gson.toJson(NoteList);
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

    @Override
    public Note addNote(Note note) throws SQLException {

        Note temp = null;
        String sqlQuery = "INSERT INTO notelender.note(week,year,name,status,text) VALUES (" +
                note.getWeek() + "," + note.getYear() + "," + note.getName() + "," + note.getStatus() + "," + note.getText() + ")";
        PreparedStatement statement = connection.prepareStatement(sqlQuery);
        statement.executeUpdate();
        ResultSet resultSet = statement.executeQuery();
        while (resultSet.next()) {
            temp = new Note(resultSet.getInt("id"), resultSet.getInt("user_id"), resultSet.getInt("group_id"), resultSet.getInt("week"), resultSet.getInt("year"), resultSet.getString("name"), resultSet.getString("status"), resultSet.getString("text"));
        }
        return temp;


    }

    @Override
    public String validateUser(String json) throws SQLException {
        System.out.println("Login is working");
        User user=null;

        System.out.println(json.toString() + "allalalalala");
        ArrayList<String> list=gson.fromJson(json,ArrayList.class);

        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.users WHERE username ='" + list.get(0) + "'");
        while (resultSet.next()) {
            user=new User(resultSet.getInt(1),resultSet.getString(2),resultSet.getString(3),resultSet.getString(4),resultSet.getString(5));
        }
        return gson.toJson(user);
    }

    @Override
    public String registerUser(String json) throws SQLException {
        System.out.println("register User is working");
        User user = gson.fromJson(json, User.class);
        List<User> userList = new ArrayList<>();
        try {
            Statement statement = connection.createStatement();
            statement.executeUpdate("INSERT INTO notelender.users (firstname,lastname,username,password) VALUES ('" + user.getFirstName() + "," + user.getLastName() + "," + user.getUsername() + "," + user.getPassword() + "')", Statement.RETURN_GENERATED_KEYS);
            try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
                if (generatedKeys.next()) {
                    User userToAdd = new User(generatedKeys.getInt(1), generatedKeys.getString(2), generatedKeys.getString(3), generatedKeys.getString(4), generatedKeys.getString(5));
                    userList.add(userToAdd);
                    return gson.toJson(userList);
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

    @Override public Invitation addInvitation(int id) throws SQLException
    {
        {
//            Invitation temp = null;
            Invitation invitation = null;
            Group group = null;

            String sqlQuery = "INSERT INTO notelender.invitation(id, invitor_id, invitee_id, group_id) VALUES (" +
                invitation.getId() + "," + invitation.getInvitorId() + "," + invitation.getInviteeId() + "," +  group.getId() + ")";
            PreparedStatement statement = connection.prepareStatement(sqlQuery);
            statement.executeUpdate();

            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                invitation = new Invitation(resultSet.getInt("id"),resultSet.getInt("invitee_id"),resultSet.getInt("invitor_id"));
            }
            return invitation;
        }
    }


    @Override public String getInvitation(int id)
        throws SQLException
    {
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

}


