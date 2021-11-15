package PersistenceService;

import PersistenceServer.Group;
import PersistenceServer.Note;
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
                    resultSet.getInt(3), resultSet.getString(4),
                    resultSet.getString(5), resultSet.getString(6));
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
            temp = new Note(resultSet.getInt("id"), resultSet.getInt("week"), resultSet.getInt("year"), resultSet.getString("name"), resultSet.getString("status"), resultSet.getString("text"));
        }
        return temp;


    }
}
