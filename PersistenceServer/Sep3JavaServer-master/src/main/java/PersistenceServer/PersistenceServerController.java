package PersistenceServer;


import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
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

    @GetMapping("/User?userName={username}&passWord={password}")
    public synchronized String ValidateUser(@PathVariable(value = "username") String username,@PathVariable(value = "password") String password ) throws SQLException {
        System.out.println("It's working Validate");
        return persistenceService.validateUser(username,password);
    }


    @PostMapping("/User")
    public synchronized String registerUser(@RequestBody String json) throws SQLException {
      return persistenceService.registerUser(json);
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
