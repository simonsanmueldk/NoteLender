package PersistenceService;


public interface IPersistenceService {
     ResponseEntity<List<Group>> getGroup(int id);
     ResponseEntity<Group> postGroup(String json);
     ResponseEntity<Void> deleteGroup(int id);
     ResponseEntity<List<GroupMembers>> getUserList(int id);
     ResponseEntity<List<Group>> getGroupList(int id);
     ResponseEntity<Void> addGroupMember(String json);
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
      String getUser(String json);
}
