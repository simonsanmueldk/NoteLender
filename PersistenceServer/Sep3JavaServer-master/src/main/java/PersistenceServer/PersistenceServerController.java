package PersistenceServer;

import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import org.springframework.web.bind.annotation.*;

@RestController
public class PersistenceServerController {

    private IPersistenceService persistenceService;

    public PersistenceServerController() {
        this.persistenceService = new PersistenceService();
        System.out.println("Hello");
    }

    @PutMapping("/group")
    public synchronized String createGroup(@RequestBody String json) {
        System.out.println("It's working Post");
        return persistenceService.postGroup(json);

    }

    @PostMapping("/user")
    public synchronized String ValidateUser(@RequestBody String json) {
        System.out.println("It's working Validate");
        return persistenceService.validateUser(json);
    }

    @PostMapping("/user/{user_id}")
    public synchronized String ValidateUser(@RequestBody String json, @PathVariable(value = "user_id") int user_id) {
        System.out.println("It's working Validate");
        return persistenceService.editUser(json, user_id);
    }


    @PostMapping("/unregisteruser")
    public synchronized String registerUser(@RequestBody String json) {
        return persistenceService.registerUser(json);
    }
    @DeleteMapping("/User/{userId}")
    public synchronized String deleteUser(@PathVariable(value = "userId") int userId){
        System.out.println("DELETE User");
        return persistenceService.deleteUser(userId);
    }



    @PostMapping("/note")
    public synchronized String addNote(@RequestBody String json) {
        System.out.println("It's working AddNote");
        return persistenceService.addNote(json);
    }

    @PutMapping("/note")
    public synchronized String editNote(@RequestBody String json) {
        System.out.println("EDIT BABYYY");
        return persistenceService.editNote(json);
    }

    @DeleteMapping("/note/{noteId}")
    public synchronized String deleteNote(@PathVariable(value = "noteId") int noteId) {
        System.out.println("DELETE NOTE");
        return persistenceService.deleteNote(noteId);
    }

    @GetMapping("/note/{groupId}")
    public synchronized String getNote(@PathVariable(value = "groupId") int groupId) {
        return persistenceService.getNote(groupId);
    }


    @GetMapping("/group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getGroup(id);
    }

    @GetMapping("/userlist/{id}")
    public synchronized String getUserList(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getUserList(id);
    }

    @GetMapping("/groupmemberslist/{id}")
    public synchronized String getGroupMembersList(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getGroupMembersList(id);
    }

    @DeleteMapping("/group/{id}")
    public synchronized String deleteGroup(@PathVariable(value = "id") int id) {
        System.out.println("It's working Delete");
        return persistenceService.deleteGroup(id);
    }

    @PostMapping("/invitation")
    public synchronized String addInvitation(@RequestBody String json) {
        System.out.println("its working post invitation");
        return persistenceService.addInvitation(json);
    }

    @GetMapping("/invitation/{id}")
    public synchronized String getInvitationList(@PathVariable(value = "id") String id) {
        System.out.println("Aleeeoooo");
        return persistenceService.getInvitationList(id);
    }

    @DeleteMapping("/invitation/{id}")
    public synchronized String deleteInvitation(@PathVariable(value = "id") String id) {
        return persistenceService.deleteInvitation(id);
    }
}
