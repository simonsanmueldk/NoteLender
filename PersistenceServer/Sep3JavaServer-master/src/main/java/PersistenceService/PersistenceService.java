package PersistenceService;

import Model.*;
import com.google.gson.Gson;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class PersistenceService implements IPersistenceService {

    private static final Gson gson = new Gson();
    private final String URL = "jdbc:postgresql://tai.db.elephantsql.com:5432/seitjdhj";
    private final String USER = "seitjdhj";
    private final String PASSWORD = "9LEmAjua_Uo0YR5sGqAFHn0Kgm9DDKu1";
    private Connection connection;

    public PersistenceService() {
        try {
            connection = DriverManager.getConnection(URL, USER, PASSWORD);
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }

    @Override
    public ResponseEntity<Void> postGroup(String json, int memberId) {
        String sqlStatement = "with groupcreation as ( insert into notelender.groups (groupname) values (?) " +
                "RETURNING id) insert into notelender.groupmembers (user_id, group_id)\n" +
                "values ( ?, (select id from groupcreation))";
        try {
            PreparedStatement insertGroup = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            insertGroup.setString(1, json);
            insertGroup.setInt(2, memberId);
            insertGroup.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);

        } catch (Exception e) {
            e.printStackTrace();

            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

//    @Override
//    public ResponseEntity<Void> postGroup(String json, int memberId) {
//        String sqlStatement = "INSERT INTO notelender.groups (groupname) VALUES (?)";
//        try {
//            connection.setAutoCommit(false);
//            PreparedStatement insertGroup = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
//            insertGroup.setString(1, json);
//            insertGroup.executeUpdate();
//            ResultSet generatedKeys = insertGroup.getGeneratedKeys();
//            if (generatedKeys.next()) {
//                String insertGroupMemberString = "INSERT INTO notelender.groupmembers (user_id, group_id)  VALUES (?,?)";
//                PreparedStatement insertMember = connection.prepareStatement(insertGroupMemberString);
//                insertMember.setInt(1, memberId);
//                insertMember.setInt(2, generatedKeys.getInt(1));
//                insertMember.executeUpdate();
//                connection.commit();
//                connection.setAutoCommit(true);
//                return new ResponseEntity<>(HttpStatus.OK);
//            } else {
//                connection.setAutoCommit(true);
//                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
//            }
//        } catch (Exception e) {
//            try {
//                connection.rollback();
//                connection.setAutoCommit(true);
//            } catch (SQLException ex) {
//                ex.printStackTrace();
//            }
//            e.printStackTrace();
//            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
//        }
//
//    }

    @Override
    public ResponseEntity<Void> editNote(String json) {
        Note note = gson.fromJson(json, Note.class);
        String sqlStatement = "UPDATE notelender.notes SET week = ?, year = ?, name = ?, status = ?, text = ? WHERE id = ?";
        try {
            PreparedStatement editNote = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            editNote.setInt(1, note.getWeek());
            editNote.setInt(2, note.getYear());
            editNote.setString(3, note.getName());
            editNote.setString(4, note.getStatus());
            editNote.setString(5, note.getText());
            editNote.setInt(6, note.getId());
            editNote.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> addNote(String json) {
        Note note = gson.fromJson(json, Note.class);
        String sqlStatement = "INSERT INTO notelender.notes (user_id,group_id,week,year,name,status,text) " +
                "VALUES (?,?,?,?,?,?,?)";
        try {
            PreparedStatement addNote = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            addNote.setInt(1, note.getUserId());
            addNote.setInt(2, note.getGroupId());
            addNote.setInt(3, note.getWeek());
            addNote.setInt(4, note.getYear());
            addNote.setString(5, note.getName());
            addNote.setString(6, note.getStatus());
            addNote.setString(7, note.getText());
            addNote.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteGroup(int id) {
        String sqlStatement = "DELETE FROM notelender.groups WHERE id= ?";
        try {
            PreparedStatement deleteGroup = connection.prepareStatement(sqlStatement);
            deleteGroup.setInt(1, id);
            deleteGroup.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<Note>> getNote(int id) {
        List<Note> NoteList = new ArrayList<>();
        try {
            String sqlStatement = "SELECT * FROM notelender.notes WHERE group_id = ? ORDER BY year ASC, week";
            PreparedStatement getNote = connection.prepareStatement(sqlStatement);
            getNote.setInt(1, id);
            ResultSet rs = getNote.executeQuery();
            while (rs.next()) {
                Note noteToAdd = new Note(rs.getInt(1), rs.getInt(2),
                        rs.getInt(3), rs.getInt(4),
                        rs.getInt(5), rs.getString(6),
                        rs.getString(7), rs.getString(8));
                NoteList.add(noteToAdd);
            }
            return new ResponseEntity<>(NoteList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<Group>> getGroup(int id) {
        List<Group> GroupList = new ArrayList<>();
        try {
            String sqlStatement = "SELECT * FROM notelender.groups WHERE id =  ?";
            PreparedStatement getGroup = connection.prepareStatement(sqlStatement);
            getGroup.setInt(1, id);
            ResultSet rs = getGroup.executeQuery();
            while (rs.next()) {
                GroupList.add(new Group(rs.getInt(1), rs.getString(2)));
            }
            return new ResponseEntity<>(GroupList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<GroupMembers>> getUserList(int id) {
        List<GroupMembers> GroupMembersList = new ArrayList<>();
        try {
            String sqlStatement = "SELECT notelender.groupmembers.id,user_id,u.username,group_id " +
                    "from notelender.groupmembers INNER JOIN notelender.users u on u.id = groupmembers.user_id " +
                    "where notelender.groupmembers.group_id = ?";
            PreparedStatement getUserList = connection.prepareStatement(sqlStatement);
            getUserList.setInt(1, id);
            ResultSet rs = getUserList.executeQuery();
            while (rs.next()) {
                GroupMembers groupMembers = new GroupMembers(rs.getInt(1), rs.getInt(2),
                        rs.getString(3), rs.getInt(4));
                GroupMembersList.add(groupMembers);
            }
            return new ResponseEntity<>(GroupMembersList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<Group>> getGroupList(int id) {
        List<Group> GroupList = new ArrayList<>();
        try {
            String sqlStatement = "SELECT g.id, groupname from notelender.groupmembers\n" +
                    " inner join notelender.groups g on g.id = groupmembers.group_id\n" +
                    "where user_id = ?";
            PreparedStatement getGroupMembersList = connection.prepareStatement(sqlStatement);
            getGroupMembersList.setInt(1, id);
            ResultSet rs = getGroupMembersList.executeQuery();
            while (rs.next()) {
                GroupList.add(new Group(rs.getInt(1), rs.getString(2)));
            }
            return new ResponseEntity<>(GroupList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> addGroupMember(String json) {
        String sqlStatement = "INSERT INTO notelender.groupmembers (user_id,group_id) VALUES (?,?)";
        GroupMembers groupMembers = gson.fromJson(json, GroupMembers.class);
        try {
            PreparedStatement addGroupMember = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            addGroupMember.setInt(1, groupMembers.getUserId());
            addGroupMember.setInt(2, groupMembers.getGroupId());
            addGroupMember.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> leaveGroup(int id, String json) {
        String sqlStatement = "DELETE FROM notelender.groupmembers WHERE group_id = ? and user_id = ?";
        try {
            PreparedStatement deleteGroupMember = connection.prepareStatement(sqlStatement);
            deleteGroupMember.setInt(1, id);
            deleteGroupMember.setInt(2, Integer.parseInt(json));
            deleteGroupMember.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteGroupMember(int id) {
        String sqlStatement = "DELETE FROM notelender.groupmembers WHERE id=?";
        try {
            PreparedStatement statement = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, id);
            statement.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<User> validateUser(String json) {
        User user = null;
        User temp = gson.fromJson(json, User.class);
        String sqlStatement = "SELECT * FROM notelender.users WHERE username =  ?";
        try {
            PreparedStatement validateUser = connection.prepareStatement(sqlStatement);
            validateUser.setString(1, temp.getUsername());
            ResultSet rs = validateUser.executeQuery();
            while (rs.next()) {
                user = new User(rs.getInt(1), rs.getString(2),
                        rs.getString(3), rs.getString(4),
                        rs.getString(5));
            }
            return new ResponseEntity<>(user, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> registerUser(String json) {
        User temp = gson.fromJson(json, User.class);
        String sqlStatement = "INSERT INTO notelender.users (firstname,lastname,username,password) VALUES (?,?,?,?)";
        try {
            PreparedStatement registerUser = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            registerUser.setString(1, temp.getFirstName());
            registerUser.setString(2, temp.getLastName());
            registerUser.setString(3, temp.getUsername());
            registerUser.setString(4, temp.getPassword());
            registerUser.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> editUser(String json, int user_id) {
        User temp = gson.fromJson(json, User.class);
        String sqlStatement = "UPDATE notelender.users SET password= ? WHERE id= ?";
        try {
            PreparedStatement editUser = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            editUser.setString(1, temp.getPassword());
            editUser.setInt(2, user_id);
            editUser.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            System.out.println(e.getMessage());
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteNote(int noteId) {
        String sqlStatement = "DELETE FROM notelender.notes WHERE id = ?";
        try {
            PreparedStatement statement = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, noteId);
            statement.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteUser(int userId) {
        String sqlStatement = "DELETE FROM notelender.user WHERE id = ?";
        try {
            PreparedStatement statement = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, userId);
            statement.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<User>> getUser(String json) {
        try {
            List<User> users = new ArrayList<>();
            String sqlStatement = "SELECT * FROM notelender.users WHERE username LIKE  ?";
            PreparedStatement getGroup = connection.prepareStatement(sqlStatement);
            getGroup.setString(1, "%" + json + "%");
            ResultSet rs = getGroup.executeQuery();
            while (rs.next()) {
                users.add(new User(rs.getInt(1), rs.getString(2),
                        rs.getString(3), rs.getString(4), null));
            }
            return new ResponseEntity<>(users, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

    }

    @Override
    public ResponseEntity<Void> addInvitation(String json) {
        {
            Invitation invitation = gson.fromJson(json, Invitation.class);
            String sqlStatement = "INSERT INTO notelender.invitations (invitor_id,invitee_id,group_id) VALUES (?,?,?)";
            try {
                PreparedStatement addInvitation = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
                addInvitation.setInt(1, invitation.getInvitorId());
                addInvitation.setInt(2, invitation.getInviteeId());
                addInvitation.setInt(3, invitation.getGroupId());
                addInvitation.executeUpdate();
                return new ResponseEntity<>(HttpStatus.OK);
            } catch (Exception e) {
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }
        }
    }

    @Override
    public ResponseEntity<Void> deleteInvitation(String id) {
        String sqlStatement = "DELETE FROM notelender.invitations WHERE id= ?";
        try {
            PreparedStatement deleteInvitation = connection.prepareStatement(sqlStatement, PreparedStatement.RETURN_GENERATED_KEYS);
            deleteInvitation.setInt(1, Integer.parseInt(id));
            deleteInvitation.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<Invitation>> getInvitationList(String id) {
        List<Invitation> InvitationList = new ArrayList<>();
        try {
            String sqlStatement = "SELECT invitations.id, g.id, g.groupname, invitations.invitee_id, u2.username, " +
                    "invitations.invitor_id, u.username FROM notelender.invitations\n" +
                    "         inner join notelender.groups g on g.id = invitations.group_id\n" +
                    "         inner join notelender.users u on u.id = invitations.invitor_id\n" +
                    "         inner join notelender.users u2 on u2.id = invitations.invitee_id\n" +
                    "WHERE invitee_id = ?";
            PreparedStatement getInvitation = connection.prepareStatement(sqlStatement);
            getInvitation.setInt(1, Integer.parseInt(id));
            ResultSet rs = getInvitation.executeQuery();
            while (rs.next()) {
                InvitationList.add(new Invitation(rs.getInt(1), rs.getInt(2),
                        rs.getString(3), rs.getInt(4), rs.getString(5),
                        rs.getInt(6), rs.getString(7)));
            }
            return new ResponseEntity<>(InvitationList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }
}


