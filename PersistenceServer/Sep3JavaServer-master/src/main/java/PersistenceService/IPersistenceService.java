package PersistenceService;

import Model.*;
import org.springframework.http.ResponseEntity;

import java.util.List;

public interface IPersistenceService {
    ResponseEntity<List<Group>> getGroup(int id);

    ResponseEntity<Void> postGroup(String json, int memberId);

    ResponseEntity<Void> deleteGroup(int id);

    ResponseEntity<List<GroupMembers>> getUserList(int id);

    ResponseEntity<List<Group>> getGroupList(int id);

    ResponseEntity<Void> addGroupMember(String json);

    ResponseEntity<Void> leaveGroup(int user_id, String json);

    ResponseEntity<Void> deleteGroupMember(int id);

    ResponseEntity<List<Note>> getNote(int groupId);

    ResponseEntity<Void> addNote(String json);

    ResponseEntity<Void> editNote(String json);

    ResponseEntity<User> validateUser(String json);

    ResponseEntity<Void> registerUser(String json);

    ResponseEntity<Void> addInvitation(String json);

    ResponseEntity<List<Invitation>> getInvitationList(String id);

    ResponseEntity<Void> deleteInvitation(String id);

    ResponseEntity<Void> editUser(String json, int user_id);

    ResponseEntity<Void> deleteNote(int noteId);

    ResponseEntity<Void> deleteUser(int userId);

    ResponseEntity<List<User>> getUsers(String json);
}
