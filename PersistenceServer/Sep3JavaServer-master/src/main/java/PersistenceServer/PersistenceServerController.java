package PersistenceServer;


import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import com.google.gson.Gson;
import org.springframework.web.bind.annotation.*;

import java.sql.*;


@RestController
public class PersistenceServerController {

    private static final Gson gson = new Gson();
    private final Connection connection =
            DriverManager.getConnection("jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj",
                    "seitjdhj", "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1");
    private IPersistenceService persistenceService;


    public PersistenceServerController() throws SQLException {
        persistenceService = new PersistenceService();
    }


    @GetMapping("/Group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
        return persistenceService.getGroup(id);
    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) throws SQLException {
        System.out.println("It's working Post");
        return persistenceService.createGroup(json);

    }
    @GetMapping("/User")
    public synchronized String ValidateUser(@RequestBody String username,@RequestBody String password ) throws SQLException {
        System.out.println("Login is working");
        User user=null;
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM notelender.users WHERE username = " + username);
        while (resultSet.next()) {
            user=new User(resultSet.getInt(1),resultSet.getString(2),resultSet.getString(3),resultSet.getString(4),resultSet.getString(5));
        }
        return gson.toJson(user);
    }


    @PostMapping("/User")
    public synchronized String registerUser(@RequestBody String json) {
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


    @PostMapping("/Note")
    public synchronized Note addNote(@RequestBody Note note) throws SQLException {
        System.out.println("It's working AddNote");
        return persistenceService.addNote(note);

    }

    @GetMapping("/NoteList/{id}")
    public synchronized String getNoteList(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working GetNoteList");
        return persistenceService.getNoteList(id);

    }

    @GetMapping("/UserList/{id}")
    public synchronized String getUserList(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
        return persistenceService.getUserList(id);

    }

    @DeleteMapping("/Group/{id}")
    public synchronized String deleteGroup(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Delete");
        return persistenceService.deleteGroup(id);


    }
}
