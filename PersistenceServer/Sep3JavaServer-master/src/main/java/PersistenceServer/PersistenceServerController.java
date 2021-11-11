package PersistenceServer;


import com.google.gson.Gson;
import org.springframework.web.bind.annotation.*;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
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
        List<Group> AdultsList = new ArrayList<>();
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.groups WHERE id = " + id);
        while (resultSet.next()) {
            Group groupToAdd = new Group(resultSet.getInt(1),resultSet.getString(2));
            AdultsList.add(groupToAdd);
        }


        return gson.toJson(AdultsList);
    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) {
        System.out.println("It's working Post");
        System.out.println(json);
        try {
            connection.createStatement().execute("INSERT INTO sep3.notes (id,string) VALUES ("
                    + '5' + ",'" + json + "')");
            return getGroup(5);
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
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
