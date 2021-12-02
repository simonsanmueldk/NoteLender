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
            System.out.println("aleoaleolaoe");
        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
    }

    @Override
    public ResponseEntity<Group> postGroup(String json) {
        Group groupToAdd = null;
        String insertString = "INSERT INTO notelender.groups (groupname) VALUES (?)";
        try {
            PreparedStatement insertGroup = connection.prepareStatement(insertString, PreparedStatement.RETURN_GENERATED_KEYS);
            insertGroup.setString(1, json);
            insertGroup.executeUpdate();
            ResultSet generatedKeys = insertGroup.getGeneratedKeys();
            if (generatedKeys.next()) {
                groupToAdd = new Group(generatedKeys.getInt(1), generatedKeys.getString(2));
            }
            return new ResponseEntity<>(groupToAdd, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

    }

    @Override
    public ResponseEntity<Void> editNote(String json) {
        Note note = gson.fromJson(json, Note.class);
        System.out.println("ALLOOOO");
        String editString =
                "UPDATE notelender.notes " +
                        "SET week = ?, year = ?, name = ?, status = ?, text = ? " +
                        "WHERE id = ?";
        try {
            PreparedStatement editNote = connection.prepareStatement(editString, PreparedStatement.RETURN_GENERATED_KEYS);
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
        String addString =
                "INSERT INTO notelender.notes (user_id,group_id,week,year,name,status,text) VALUES (?,?,?,?,?,?,?)";
        System.out.println("JSON: " + json);
        Note note = gson.fromJson(json, Note.class);
        System.out.println("Note: " + note.getId());
        try {
            PreparedStatement addNote = connection.prepareStatement(addString, PreparedStatement.RETURN_GENERATED_KEYS);
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
        String deleteString = "DELETE FROM notelender.groups WHERE id= ?";
        try {
            PreparedStatement deleteGroup = connection.prepareStatement(deleteString);
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
            String getString = "SELECT * FROM notelender.notes WHERE group_id = ? ORDER BY year ASC, week";
            PreparedStatement getNote = connection.prepareStatement(getString);
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
            String getString = "SELECT * FROM notelender.groups WHERE id =  ?";
            PreparedStatement getGroup = connection.prepareStatement(getString);
            getGroup.setInt(1, id);
            ResultSet rs = getGroup.executeQuery();
            while (rs.next()) {
                Group groupToAdd = new Group(rs.getInt(1), rs.getString(2));
                GroupList.add(groupToAdd);
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
            String getString = "SELECT notelender.groupmembers.id,user_id,u.username,group_id from notelender.groupmembers INNER JOIN notelender.users u on u.id = groupmembers.user_id where notelender.groupmembers.group_id = ?";
            PreparedStatement getUserList = connection.prepareStatement(getString);
            getUserList.setInt(1, id);
            ResultSet rs = getUserList.executeQuery();
            while (rs.next()) {
                GroupMembers groupMembers = new GroupMembers(rs.getInt(1), rs.getInt(2), rs.getString(3), rs.getInt(4));
                GroupMembersList.add(groupMembers);
                System.out.println(groupMembers.getUsername());
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
        System.out.println("hi " + id);
        try {
            String getString = "SELECT g.id, groupname from notelender.groupmembers\n" +
                    "         inner join notelender.groups g on g.id = groupmembers.group_id\n" +
                    "where user_id = ?";
            PreparedStatement getGroupMembersList = connection.prepareStatement(getString);
            getGroupMembersList.setInt(1, id);
            ResultSet rs = getGroupMembersList.executeQuery();
            while (rs.next()) {
                Group group = new Group(rs.getInt(1), rs.getString(2));
                GroupList.add(group);
            }
            return new ResponseEntity<>(GroupList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> addGroupMember(String json) {
        String addString =
                "INSERT INTO notelender.groupmembers (user_id,group_id) VALUES (?,?)";
        System.out.println("JSON: " + json);
        GroupMembers groupMembers = gson.fromJson(json, GroupMembers.class);
        System.out.println("GroupMembers: " + groupMembers.getId() + " " + groupMembers.getUserId() + " " + groupMembers.getUsername() + " " + groupMembers.getGroupId());
        try {
            PreparedStatement addGroupMember = connection.prepareStatement(addString, PreparedStatement.RETURN_GENERATED_KEYS);
            addGroupMember.setInt(1, groupMembers.getUserId());
            addGroupMember.setInt(2, groupMembers.getGroupId());
            addGroupMember.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<User> validateUser(String json) {
        User user = null;
        User temp = gson.fromJson(json, User.class);
        System.out.println(temp.getUsername());
        String getString = "SELECT * FROM notelender.users WHERE username =  ?";
        try {
            PreparedStatement validateUser = connection.prepareStatement(getString);
            validateUser.setString(1, temp.getUsername());
            ResultSet rs = validateUser.executeQuery();
            while (rs.next()) {
                user = new User(rs.getInt(1), rs.getString(2),
                        rs.getString(3), rs.getString(4),
                        rs.getString(5));
            }
            System.out.println("Login is working");
            return new ResponseEntity<>(user, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> registerUser(String json) {
        User temp = gson.fromJson(json, User.class);
        String registerString = "INSERT INTO notelender.users (firstname,lastname,username,password) VALUES (?,?,?,?)";
        try {
            PreparedStatement registerUser = connection.prepareStatement(registerString, PreparedStatement.RETURN_GENERATED_KEYS);
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
        String editString = "UPDATE notelender.users SET password= ? + WHERE id= ?";
        try {
            PreparedStatement editUser = connection.prepareStatement(editString, PreparedStatement.RETURN_GENERATED_KEYS);
            editUser.setString(1, temp.getPassword());
            editUser.setInt(2, temp.getId());
            editUser.executeUpdate();

            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteNote(int noteId) {
        String sql = "DELETE FROM notelender.notes WHERE id = ?";

        try {
            PreparedStatement statement = connection.prepareStatement(sql, PreparedStatement.RETURN_GENERATED_KEYS);
            statement.setInt(1, noteId);
            statement.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<Void> deleteUser(int userId) {
        String sql = "DELETE FROM notelender.user WHERE id = ?";
        try {
            PreparedStatement statement = connection.prepareStatement(sql, PreparedStatement.RETURN_GENERATED_KEYS);
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
            List<User> users=new ArrayList<>();
            User userToAdd=null;
            String getString = "SELECT * FROM notelender.users WHERE username LIKE  ?";
            PreparedStatement getGroup = connection.prepareStatement(getString);
            getGroup.setString(1,"%"+ json+"%");
            ResultSet rs = getGroup.executeQuery();

            while (rs.next()) {
                userToAdd =new User(rs.getInt(1), rs.getString(2),
                        rs.getString(3), rs.getString(4),null);
                users.add(userToAdd);
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
            System.out.println("JSON: " + json);
            Invitation invitation = gson.fromJson(json, Invitation.class);
            String addString = "INSERT INTO notelender.invitations (invitor_id,invitee_id,group_id) VALUES (?,?,?)";
            try {
                PreparedStatement addInvitation = connection.prepareStatement(addString, PreparedStatement.RETURN_GENERATED_KEYS);
                addInvitation.setInt(1, invitation.getInvitorId());
                addInvitation.setInt(2, invitation.getInviteeId());
                addInvitation.setInt(3, invitation.getGroupId());
                addInvitation.executeUpdate();
                System.out.println("aleoooo");
                return new ResponseEntity<>(HttpStatus.OK);
            } catch (Exception e) {
                System.out.println("aleo x2");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }
        }
    }

    @Override
    public ResponseEntity<Void> deleteInvitation(String id) {
        String deleteString = "DELETE FROM notelender.invitations WHERE id= ?";
        try {
            PreparedStatement deleteInvitation = connection.prepareStatement(deleteString, PreparedStatement.RETURN_GENERATED_KEYS);
            deleteInvitation.setInt(1, Integer.parseInt(id));
            deleteInvitation.executeUpdate();
            return new ResponseEntity<>(HttpStatus.OK);
        } catch (Exception e) {
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }

    @Override
    public ResponseEntity<List<Invitation>> getInvitationList(String id) {
        System.out.println("getInvitation");
        List<Invitation> InvitationList = new ArrayList<>();
        try {
            String getString = "SELECT invitations.id, g.id, g.groupname, invitations.invitee_id, u2.username, " +
                    "invitations.invitor_id, u.username\n" +
                    "FROM notelender.invitations\n" +
                    "         inner join notelender.groups g on g.id = invitations.group_id\n" +
                    "         inner join notelender.users u on u.id = invitations.invitor_id\n" +
                    "         inner join notelender.users u2 on u2.id = invitations.invitee_id\n" +
                    "WHERE invitee_id = ?";
            PreparedStatement getInvitation = connection.prepareStatement(getString);
            getInvitation.setInt(1, Integer.parseInt(id));
            ResultSet rs = getInvitation.executeQuery();
            while (rs.next()) {
                Invitation invitationToAdd = new Invitation(rs.getInt(1), rs.getInt(2),
                        rs.getString(3), rs.getInt(4),
                        rs.getString(5), rs.getInt(6),
                        rs.getString(7));
                InvitationList.add(invitationToAdd);
            }
            return new ResponseEntity<>(InvitationList, HttpStatus.OK);
        } catch (Exception e) {
            e.printStackTrace();
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }
    }
}


