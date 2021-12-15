package PersistenceService;

import Model.*;
import org.springframework.http.ResponseEntity;

import java.util.List;

public interface IPersistenceService {

    ResponseEntity<Void> addInvitation(String requestBody);

    ResponseEntity<Void> postGroup(String requestBody, int memberId);

    ResponseEntity<Void> deleteGroup(int id);

    ResponseEntity<List<GroupMembers>> getGroupMemberList(int id);

    ResponseEntity<List<Group>> getGroupList(int id);

    ResponseEntity<Void> addGroupMember(String requestBody);

    ResponseEntity<Void> leaveGroup(int user_id, String requestBody);

    ResponseEntity<Void> deleteGroupMember(int id);

    ResponseEntity<List<Note>> getNoteList(int groupId);

    ResponseEntity<Void> addNote(String requestBody);

    ResponseEntity<Void> editNote(String requestBody);

    ResponseEntity<User> validateUser(String requestBody);

    ResponseEntity<Void> registerUser(String requestBody);

    ResponseEntity<List<Invitation>> getInvitationList(int id);

    ResponseEntity<Void> deleteInvitation(int id);

    ResponseEntity<Void> editUser(String requestBody, int user_id);

    ResponseEntity<Void> deleteNote(int noteId);

    ResponseEntity<Void> deleteUser(int userId);

    ResponseEntity<List<User>> getUserList(String requestBody);
}
