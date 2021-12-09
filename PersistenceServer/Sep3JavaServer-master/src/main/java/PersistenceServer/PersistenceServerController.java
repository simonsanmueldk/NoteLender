package PersistenceServer;

import Model.*;
import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class PersistenceServerController {

    private final IPersistenceService persistenceService;

    public PersistenceServerController() {
        this.persistenceService = new PersistenceService();
    }

    @PostMapping("/note")
    public ResponseEntity<Void> addNote(@RequestBody String json) {
        System.out.println("It's working AddNote");
        return persistenceService.addNote(json);
    }

    @PutMapping("/note")
    public ResponseEntity<Void> editNote(@RequestBody String json) {
        System.out.println("EDIT BABYYY");
        return persistenceService.editNote(json);
    }

    @DeleteMapping("/note/{noteId}")
    public ResponseEntity<Void> deleteNote(@PathVariable(value = "noteId") int noteId) {
        System.out.println("DELETE NOTE");
        return persistenceService.deleteNote(noteId);
    }

    @GetMapping("/note/{groupId}")
    public ResponseEntity<List<Note>> getNote(@PathVariable(value = "groupId") int groupId) {
        return persistenceService.getNotes(groupId);
    }

    @PutMapping("/group/{memberId}")
    public ResponseEntity<Void> createGroup(@RequestBody String json, @PathVariable(value = "memberId") int memberId) {
        System.out.println("It's working Post");
        return persistenceService.postGroup(json, memberId);
    }

//    @GetMapping("/group/{id}")
//    public ResponseEntity<List<Group>> getGroup(@PathVariable(value = "id") int id) {
//        System.out.println("It's working Get");
//        return persistenceService.getGroup(id);
//    }

    @GetMapping("/groupmemberslist/{id}")
    public ResponseEntity<List<Group>> getGroupList(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getGroupList(id);
    }

    @DeleteMapping("/group/{id}")
    public ResponseEntity<Void> deleteGroup(@PathVariable(value = "id") int id) {
        System.out.println("It's working Deleted");
        return persistenceService.deleteGroup(id);
    }

    @PostMapping("/groupmembers/{id}")
    public ResponseEntity<Void> LeaveGroup(@PathVariable(value = "id") int id, @RequestBody String json) {
        return persistenceService.leaveGroup(id, json);
    }

    @GetMapping("/users/{username}")
    public ResponseEntity<List<User>> getUsers(@PathVariable(value = "username") String username) {
        System.out.println("Get user");
        return persistenceService.getUsers(username);
    }

    @PostMapping("/user")
    public ResponseEntity<User> ValidateUser(@RequestBody String json) {
        System.out.println("It's working Validate");
        return persistenceService.validateUser(json);
    }

    @PostMapping("/unregisteruser")
    public ResponseEntity<Void> registerUser(@RequestBody String json) {
        return persistenceService.registerUser(json);
    }


    @PostMapping("/user/{user_id}")
    public ResponseEntity<Void> EditUser(@RequestBody String json, @PathVariable(value = "user_id") int user_id) {
        System.out.println("It's working Validate");
        return persistenceService.editUser(json, user_id);
    }


    @DeleteMapping("/user/{userId}")
    public ResponseEntity<Void> deleteUser(@PathVariable(value = "userId") int userId) {
        System.out.println("DELETE User");
        return persistenceService.deleteUser(userId);
    }

    @GetMapping("/userlist/{id}")
    public ResponseEntity<List<GroupMembers>> getUserList(@PathVariable(value = "id") int id) {
        System.out.println("It's working Get");
        return persistenceService.getUserList(id);
    }

    @DeleteMapping("/groupmembers/{id}")
    public ResponseEntity<Void> deleteGroupMember(@PathVariable(value = "id") int id) {
        System.out.println("It's working Delete groupMember");
        return persistenceService.deleteGroupMember(id);
    }



    @PostMapping("/invitation")
    public ResponseEntity<Void> addInvitation(@RequestBody String json) {
        System.out.println("its working post invitation");
        return persistenceService.addInvitation(json);
    }

    @GetMapping("/invitation/{id}")
    public ResponseEntity<List<Invitation>> getInvitationList(@PathVariable(value = "id") int id) {
        return persistenceService.getInvitationList(id);
    }

    @DeleteMapping("/invitation/{id}")
    public ResponseEntity<Void> deleteInvitation(@PathVariable(value = "id") int id) {
        return persistenceService.deleteInvitation(id);
    }

    @PostMapping("/groupmembers")
    public ResponseEntity<Void> AddGroupMember(@RequestBody String json) {
        return persistenceService.addGroupMember(json);
    }
}