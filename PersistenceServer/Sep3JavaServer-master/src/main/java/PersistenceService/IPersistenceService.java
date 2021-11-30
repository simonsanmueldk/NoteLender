package PersistenceService;


public interface IPersistenceService {
     String getGroup(int id);
     String postGroup(String json);
     String deleteGroup(int id);
     String getUserList(int id);
     String getGroupMembersList(int id);
     String addGroupMember(String json);
     String getNote(int groupId);
     String addNote(String json);
     String editNote(String json);
     String validateUser(String json);
     String registerUser(String json);
     String addInvitation(String json);
     String getInvitationList(String id);
     String deleteInvitation(String id);
     String editUser(String json, int user_id);
     String deleteNote(int noteId);
     String deleteUser(int userId);
}
