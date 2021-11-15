package PersistenceServer;



import com.google.gson.Gson;
import org.springframework.web.bind.annotation.*;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;


@RestController
public class PersistenceServerController {

    private static final Gson gson = new Gson();
    private final Connection connection =
            DriverManager.getConnection("jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj",
                    "seitjdhj", "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1");


    public PersistenceServerController() throws SQLException {
    }



    @GetMapping("/Group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
        List<Group> GroupList = new ArrayList<>();
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.groups WHERE id = " + id);
        while (resultSet.next()) {
            Group groupToAdd = new Group(resultSet.getInt(1), resultSet.getString(2));
            GroupList.add(groupToAdd);
        }
        return gson.toJson(GroupList);
    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) {
        System.out.println("Dorin sexy");
        System.out.println("It's working Post");
        System.out.println(json);
        List<Group> GroupList = new ArrayList<>();
        try {
            Statement statement = connection.createStatement();
            statement.executeUpdate("INSERT INTO notelender.groups (name) VALUES ('"+  json+"')", Statement.RETURN_GENERATED_KEYS);
            try (ResultSet generatedKeys = statement.getGeneratedKeys()) {
                if (generatedKeys.next()) {
                    Group groupToAdd = new Group(generatedKeys.getInt(1),generatedKeys.getString(2));
                    GroupList.add(groupToAdd);
                    return gson.toJson(GroupList);
                }
                else {
                    throw new SQLException("Creating failed, no ID obtained.");
                }
            }
        }
        catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
    }

    @PostMapping("/Note")
    public synchronized Note addNote(@RequestBody Note note) {
        try {
            Note temp = null;
            PreparedStatement statement = connection.prepareStatement("INSERT INTO notelender.note(week,year,name,status,text) VALUES (" +
                    note.getWeek() + "," + note.getYear() + "," + note.getName() + "," + note.getStatus() + "," + note.getText() + ")");
            statement.executeUpdate();
            ResultSet resultSet = statement.executeQuery();
            while (resultSet.next()) {
                temp = new Note(resultSet.getInt("id"), resultSet.getInt("week"), resultSet.getInt("year"), resultSet.getString("name"), resultSet.getString("status"), resultSet.getString("text"));
            }
            return temp;
        } catch (SQLException e) {
            System.out.println("Connection failure.");
           return null;
        }

    }

    @GetMapping("/NoteList/{id}")
    public synchronized String getNoteList(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working GetNoteList");
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

    @GetMapping("/UserList/{id}")
    public synchronized String getUserList(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
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
}
