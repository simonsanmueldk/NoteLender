package PersistenceService;

import java.sql.SQLException;

public interface IPersistenceService {
     String getGroup(int id) throws SQLException;
     String createGroup(String json) throws SQLException;
     String deleteGroup(int id) throws SQLException;
     String getNoteList(int id) throws SQLException;
     String getUserList(int id) throws SQLException;
     String getNote(int groupId, int noteId) throws SQLException;
     String addNote(String json) throws SQLException;
     String validateUser(String json) throws SQLException;
     String registerUser(String json) throws SQLException;
     String addInvitation(int id) throws  SQLException;
     String getInvitation(int id) throws SQLException;

}
