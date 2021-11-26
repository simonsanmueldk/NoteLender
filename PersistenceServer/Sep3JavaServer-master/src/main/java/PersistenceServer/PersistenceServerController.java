package PersistenceServer;


import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import org.springframework.web.bind.annotation.*;

import java.sql.*;

@RestController
public class PersistenceServerController {

    private IPersistenceService persistenceService;


    public PersistenceServerController() throws SQLException {
        persistenceService = new PersistenceService();


        System.out.println("Hello");

    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) throws SQLException {
        System.out.println("It's working Post");
        return persistenceService.postGroup(json);

    }

    @PostMapping("/User")
    public synchronized String ValidateUser(@RequestBody String json )  throws SQLException {
        System.out.println("It's working Validate");
        return persistenceService.validateUser(json);
    }


    @PostMapping("/UnregisterUser")
    public synchronized String registerUser(@RequestBody String json) throws SQLException {
      return persistenceService.registerUser(json);
    }


    @PostMapping("/Note")
    public synchronized String addNote(@RequestBody String json) throws SQLException {
        System.out.println("It's working AddNote");
        return persistenceService.addNote(json);

    }

    @GetMapping("/Note/{groupId}")
    public synchronized String getNote(@PathVariable(value = "groupId") int groupId) throws SQLException{
        return persistenceService.getNote(groupId);
    }

    @GetMapping("/Group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
        return persistenceService.getGroup(id);
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
    @PostMapping("/Invitation")
    public synchronized  String addInvitation(@RequestBody String json) throws SQLException
    {
        return  persistenceService.addInvitation(json);
    }

    @GetMapping("/InvitationList/{id}")
    public synchronized String getInvitationList(@PathVariable(value = "id") int id) throws SQLException {
        return persistenceService.getInvitation(id);
    }
}
