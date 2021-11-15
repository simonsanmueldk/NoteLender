package PersistenceService;

import PersistenceServer.Note;

import java.sql.SQLException;

public interface IPersistenceService {
    public String getGroup(int id) throws SQLException;
    public String createGroup(String json) throws SQLException;
    public String deleteGroup(int id) throws SQLException;
    public String getNoteList(int id) throws SQLException;
    public String getUserList(int id) throws SQLException;
    public Note addNote(Note note) throws SQLException;
}
