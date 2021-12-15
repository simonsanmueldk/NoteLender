package PersistenceServer;

import Model.*;
import PersistenceService.IPersistenceService;
import PersistenceService.PersistenceService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class PersistenceServerController {

    /**
     * Instance variable
     */
    private final IPersistenceService persistenceService;

    /**
     * Constructor for PersistenceServerController
     */
    public PersistenceServerController() {
        this.persistenceService = new PersistenceService();
    }

    /**
     * Get note list
     * @param groupId
     * @return persistenceService.getNoteList(groupId)
     */
    @GetMapping("/notes/{groupId}")
    public ResponseEntity<List<Note>> getNoteList(@PathVariable(value = "groupId") int groupId) {
        return persistenceService.getNoteList(groupId);
    }

    /**
     * Add note
     * @param requestBody
     * @return persistenceService.addNote(requestBody)
     */
    @PostMapping("/note")
    public ResponseEntity<Void> addNote(@RequestBody String requestBody) {
        return persistenceService.addNote(requestBody);
    }

    /**
     * Edit note
     * @param requestBody
     * @return persistenceService.editNote(requestBody)
     */
    @PutMapping("/note")
    public ResponseEntity<Void> editNote(@RequestBody String requestBody) {
        return persistenceService.editNote(requestBody);
    }

    /**
     * Delete note
     * @param noteId
     * @return persistenceService.deleteNote(noteId)
     */
    @DeleteMapping("/note/{noteId}")
    public ResponseEntity<Void> deleteNote(@PathVariable(value = "noteId") int noteId) {
        return persistenceService.deleteNote(noteId);
    }

    /**
     * Get group list
     * @param id
     * @return persistenceService.getGroupList(id)
     */
    @GetMapping("/groups/{id}")
    public ResponseEntity<List<Group>> getGroupList(@PathVariable(value = "id") int id) {
        return persistenceService.getGroupList(id);
    }

    /**
     * Create grouå
     * @param requestBody
     * @param memberId
     * @return persistenceService.postGroup(requestBody, memberId)
     */
    @PostMapping("/group/{memberId}")
    public ResponseEntity<Void> createGroup(@RequestBody String requestBody, @PathVariable(value = "memberId") int memberId) {
        return persistenceService.postGroup(requestBody, memberId);
    }

    /**
     * Delete group
     * @param id
     * @return persistenceService.deleteGroup(id)
     */
    @DeleteMapping("/group/{id}")
    public ResponseEntity<Void> deleteGroup(@PathVariable(value = "id") int id) {
        return persistenceService.deleteGroup(id);
    }

    /**
     * Leave group
     * @param id
     * @param requestBody
     * @return
     */
    @PostMapping("/groupmembers/{id}")
    public ResponseEntity<Void> LeaveGroup(@PathVariable(value = "id") int id, @RequestBody String requestBody) {
        return persistenceService.leaveGroup(id, requestBody);
    }

    /**
     * Get userList
     * @param username
     * @return persistenceService.getUserList(username)
     */
    @GetMapping("/users/{username}")
    public ResponseEntity<List<User>> getUserList(@PathVariable(value = "username") String username) {
        return persistenceService.getUserList(username);
    }

    /**
     * Validate user
     * @param requestBody
     * @return persistenceService.validateUser(requestBody)
     */
    @PostMapping("/user")
    public ResponseEntity<User> ValidateUser(@RequestBody String requestBody) {
        return persistenceService.validateUser(requestBody);
    }

    /**
     * Registers user
     * @param requestBody
     * @return persistenceService.registerUser(requestBody)
     */
    @PostMapping("/unregisteruser")
    public ResponseEntity<Void> registerUser(@RequestBody String requestBody) {
        return persistenceService.registerUser(requestBody);
    }

    /**
     * Edit user
     * @param requestBody
     * @param user_id
     * @return persistenceService.editUser(requestBody, user_id)
     */
    @PostMapping("/user/{user_id}")
    public ResponseEntity<Void> EditUser(@RequestBody String requestBody, @PathVariable(value = "user_id") int user_id) {
        return persistenceService.editUser(requestBody, user_id);
    }

    /**
     * Delete user
     * @param userId
     * @return persistenceService.deleteUser(userId)
     */
    @DeleteMapping("/user/{userId}")
    public ResponseEntity<Void> deleteUser(@PathVariable(value = "userId") int userId) {
        return persistenceService.deleteUser(userId);
    }

    /**
     * Get GroupMemberList
     * @param id
     * @return persistenceService.getGroupMemberList(id)
     */
    @GetMapping("/groupmemberslist/{id}")
    public ResponseEntity<List<GroupMembers>> getGroupMemberList(@PathVariable(value = "id") int id) {
        return persistenceService.getGroupMemberList(id);
    }

    /**
     * Delete group member
     * @param id
     * @return persistenceService.deleteGroupMember(id)
     */
    @DeleteMapping("/groupmembers/{id}")
    public ResponseEntity<Void> deleteGroupMember(@PathVariable(value = "id") int id) {
        return persistenceService.deleteGroupMember(id);
    }

    /**
     * Add invitation
     * @param requestBody
     * @return persistenceService.addInvitation(requestBody)
     */

    @PostMapping("/invitation")
    public ResponseEntity<Void> addInvitation(@RequestBody String requestBody) {
        return persistenceService.addInvitation(requestBody);
    }

    /**
     * Get invitationlist
     * @param id
     * @return persistenceService.getInvitationList(id)
     */
    @GetMapping("/invitations/{id}")
    public ResponseEntity<List<Invitation>> getInvitationList(@PathVariable(value = "id") int id) {
        return persistenceService.getInvitationList(id);
    }

    /**
     * Delete invitation
     * @param id
     * @return persistenceService.deleteInvitation(id)
     */
    @DeleteMapping("/invitation/{id}")
    public ResponseEntity<Void> deleteInvitation(@PathVariable(value = "id") int id) {
        return persistenceService.deleteInvitation(id);
    }

    /**
     * Add group member
     * @param requestBody
     * @return persistenceService.addGroupMember(requestBody)
     */
    @PostMapping("/groupmembers")
    public ResponseEntity<Void> AddGroupMember(@RequestBody String requestBody) {
        return persistenceService.addGroupMember(requestBody);
    }
}