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

    @GetMapping("/notes/{groupId}")
    public ResponseEntity<List<Note>> getNoteList(@PathVariable(value = "groupId") int groupId) {
        return persistenceService.getNoteList(groupId);
    }

    @PostMapping("/note")
    public ResponseEntity<Void> addNote(@RequestBody String requestBody) {
        return persistenceService.addNote(requestBody);
    }

    @PutMapping("/note")
    public ResponseEntity<Void> editNote(@RequestBody String requestBody) {
        return persistenceService.editNote(requestBody);
    }

    @DeleteMapping("/note/{noteId}")
    public ResponseEntity<Void> deleteNote(@PathVariable(value = "noteId") int noteId) {
        return persistenceService.deleteNote(noteId);
    }

    @GetMapping("/groups/{id}")
    public ResponseEntity<List<Group>> getGroupList(@PathVariable(value = "id") int id) {
        return persistenceService.getGroupList(id);
    }

    @PostMapping("/group/{memberId}")
    public ResponseEntity<Void> createGroup(@RequestBody String requestBody, @PathVariable(value = "memberId") int memberId) {
        return persistenceService.postGroup(requestBody, memberId);
    }

    @DeleteMapping("/group/{id}")
    public ResponseEntity<Void> deleteGroup(@PathVariable(value = "id") int id) {
        return persistenceService.deleteGroup(id);
    }

    @PostMapping("/groupmembers/{id}")
    public ResponseEntity<Void> LeaveGroup(@PathVariable(value = "id") int id, @RequestBody String requestBody) {
        return persistenceService.leaveGroup(id, requestBody);
    }

    @GetMapping("/users/{username}")
    public ResponseEntity<List<User>> getUserList(@PathVariable(value = "username") String username) {
        return persistenceService.getUserList(username);
    }

    @PostMapping("/user")
    public ResponseEntity<User> ValidateUser(@RequestBody String requestBody) {
        return persistenceService.validateUser(requestBody);
    }

    @PostMapping("/unregisteruser")
    public ResponseEntity<Void> registerUser(@RequestBody String requestBody) {
        return persistenceService.registerUser(requestBody);
    }


    @PostMapping("/user/{user_id}")
    public ResponseEntity<Void> EditUser(@RequestBody String requestBody, @PathVariable(value = "user_id") int user_id) {
        return persistenceService.editUser(requestBody, user_id);
    }


    @DeleteMapping("/user/{userId}")
    public ResponseEntity<Void> deleteUser(@PathVariable(value = "userId") int userId) {
        return persistenceService.deleteUser(userId);
    }

    @GetMapping("/groupmemberslist/{id}")
    public ResponseEntity<List<GroupMembers>> getGroupMemberList(@PathVariable(value = "id") int id) {
        return persistenceService.getGroupMemberList(id);
    }

    @DeleteMapping("/groupmembers/{id}")
    public ResponseEntity<Void> deleteGroupMember(@PathVariable(value = "id") int id) {
        return persistenceService.deleteGroupMember(id);
    }

    @PostMapping("/groupmembers")
    public ResponseEntity<Void> AddGroupMember(@RequestBody String requestBody) {
        return persistenceService.addGroupMember(requestBody);
    }

    @PostMapping("/invitation")
    public ResponseEntity<Void> addInvitation(@RequestBody String requestBody) {
        return persistenceService.addInvitation(requestBody);
    }

    @GetMapping("/invitations/{id}")
    public ResponseEntity<List<Invitation>> getInvitationList(@PathVariable(value = "id") int id) {
        return persistenceService.getInvitationList(id);
    }

    @DeleteMapping("/invitation/{id}")
    public ResponseEntity<Void> deleteInvitation(@PathVariable(value = "id") int id) {
        return persistenceService.deleteInvitation(id);
    }


}