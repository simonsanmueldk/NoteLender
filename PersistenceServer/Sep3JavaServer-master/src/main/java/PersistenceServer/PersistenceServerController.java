package PersistenceServer;


import DatabaseModels.GroupModel;
import JpaRepositories.GroupRepository;
import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import PersistenceService.GroupService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.validation.annotation.Validated;
import org.springframework.web.bind.annotation.*;

import java.util.List;


@RestController
public class PersistenceServerController {

    @Autowired
    private GroupService groupService;

    private IPersistenceService persistenceService;

    public PersistenceServerController() {
        this.persistenceService = new PersistenceService();
        System.out.println("Hello");
    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) {
        System.out.println("It's working Post");
        return persistenceService.postGroup(json);

    }

    @PostMapping("/User")
    public synchronized String ValidateUser(@RequestBody String json) {
        System.out.println("It's working Validate");
        return persistenceService.validateUser(json);
    }

    @PostMapping("/User/{user_id}")
    public synchronized String ValidateUser(@RequestBody String json, @PathVariable(value = "user_id") int user_id) {
        System.out.println("It's working Validate");
        return persistenceService.editUser(json, user_id);
    }


    @PostMapping("/UnregisterUser")
    public synchronized String registerUser(@RequestBody String json) {
        return persistenceService.registerUser(json);
    }
    @DeleteMapping("/User/{userId}")
    public synchronized String deleteUser(@PathVariable(value = "userId") int userId){
        System.out.println("DELETE User");
        return persistenceService.deleteUser(userId);
    }



    @PostMapping("/Note")
    public synchronized String addNote(@RequestBody String json) {
        System.out.println("It's working AddNote");
        return persistenceService.addNote(json);
    }

    @PutMapping("/Note")
    public synchronized String editNote(@RequestBody String json) {
        System.out.println("EDIT BABYYY");
        return persistenceService.editNote(json);
    }

    @DeleteMapping("/Note/{noteId}")
    public synchronized String deleteNote(@PathVariable(value = "noteId") int noteId) {
        System.out.println("DELETE NOTE");
        return persistenceService.deleteNote(noteId);
    }

    @GetMapping("/Note/{groupId}")
    public synchronized String getNote(@PathVariable(value = "groupId") int groupId) {
        return persistenceService.getNote(groupId);
    }

    @GetMapping("/group")
    public List<GroupModel> getGroups() {
        return groupService.getAllGroups();
    }

    @GetMapping("/Group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getGroup(id);
    }

    @GetMapping("/UserList/{id}")
    public synchronized String getUserList(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getUserList(id);
    }

    @DeleteMapping("/Group/{id}")
    public synchronized String deleteGroup(@PathVariable(value = "id") int id) {
        System.out.println("It's working Delete");
        return persistenceService.deleteGroup(id);
    }

    @PostMapping("/Invitation")
    public synchronized String addInvitation(@RequestBody String json) {
        System.out.println("its working post invitation");
        return persistenceService.addInvitation(json);
    }

    @GetMapping("/Invitation/{id}")
    public synchronized String getInvitationList(@PathVariable(value = "id") String id) {
        System.out.println("Aleeeoooo");
        return persistenceService.getInvitationList(id);
    }

    @DeleteMapping("/Invitation/{id}")
    public synchronized String deleteInvitation(@PathVariable(value = "id") String id) {
        return persistenceService.deleteInvitation(id);
    }
}
