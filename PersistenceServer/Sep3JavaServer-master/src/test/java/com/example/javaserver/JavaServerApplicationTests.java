package com.example.javaserver;

import Model.*;
import PersistenceServer.PersistenceServer;
import PersistenceServer.PersistenceServerController;
import com.google.gson.Gson;
import org.junit.jupiter.api.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.util.ArrayList;
import java.util.List;

import static org.springframework.test.web.servlet.result.MockMvcResultHandlers.print;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.content;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.status;


@RunWith(SpringRunner.class)
@ContextConfiguration(classes = PersistenceServer.class)
@WebMvcTest(PersistenceServerController.class)
class JavaServerApplicationTests {

    private static final Gson gson = new Gson();
    @Autowired
    private MockMvc mockMvc;

    @Test
    public void getNotesTest() throws Exception {
        Note note = new Note(17, 76, 49, 2021, "wqe", "Started", "qew");
        List<Note> noteList = new ArrayList<>();
        noteList.add(note);
        String str = gson.toJson(noteList);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .get("/note/{groupId}", 76)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }

    @Test
    public void addNotesTest() throws Exception {
        Note note = new Note(17, 77, 49, 2021, "wqe", "Started", "qew");
        String str = gson.toJson(note);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/note")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void editNotesTest() throws Exception {
        Note note = new Note(20, 77, 33, 2021, "wqe", "Finished", "qew");
        String str = gson.toJson(note);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .put("/note")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void deleteNotesTest() throws Exception {
        this.mockMvc.perform(MockMvcRequestBuilders
                        .delete("/note/{noteId}", 20)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void getUserListTest() throws Exception {
        GroupMembers groupMembers = new GroupMembers(15, 8, "r", 77);
        List<GroupMembers> groupMembersList = new ArrayList<>();
        groupMembersList.add(groupMembers);
        String str = gson.toJson(groupMembersList);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .get("/userlist/{id}", 77)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }

    @Test
    public void createGroupTest() throws Exception {
        String str = "Testing Group";
        this.mockMvc.perform(MockMvcRequestBuilders
                        .put("/group/{memberId}", 3)
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void getGroupListTest() throws Exception {
        Group group = new Group(78, "r3");
        List<Group> groupList = new ArrayList<>();
        groupList.add(group);
        String str = gson.toJson(groupList);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .get("/groupmemberslist/{id}", 3)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }


    @Test
    public void deleteGroupTest() throws Exception {

        this.mockMvc.perform(MockMvcRequestBuilders
                        .delete("/group/{id}", 74)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void leaveGroupTest() throws Exception {
        String str = "8";
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/groupmembers/{id}", 77)
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void getUsersTest() throws Exception {
        User user = new User(5, "Test", "Test", "test1", null);
        List<User> userList = new ArrayList<>();
        userList.add(user);
        String str = gson.toJson(userList);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .get("/users/{username}", "test1")
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }

    @Test
    public void validateUserTest() throws Exception {
        User user = new User(5, "Test", "Test", "test1", "test");
        String str = gson.toJson(user);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/user")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }

    @Test
    public void registerUserTest() throws Exception {
        User user = new User(5, "Tester", "Tester", "Tester1", "Tester");
        String str = gson.toJson(user);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/unregisteruser")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void editUserTest() throws Exception {
        User user = new User(5, "Test", "Test", "test1", "test1");
        String str = gson.toJson(user);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/user/{user_id}", 5)
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void deleteUserTest() throws Exception {
        this.mockMvc.perform(MockMvcRequestBuilders
                        .delete("/user/{user_id}", 32)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void deleteGroupMemberTest() throws Exception {
        this.mockMvc.perform(MockMvcRequestBuilders
                        .delete("/groupmembers/{id}", 20)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void addGroupMemberTest() throws Exception {
        GroupMembers groupMembers = new GroupMembers(15, 8, "r", 75);
        String str = gson.toJson(groupMembers);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/groupmembers")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void addInvitationTest() throws Exception {
        Invitation invitation = new Invitation(26, 73, "No", 8, "r", 3, "esben");
        String str = gson.toJson(invitation);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .post("/invitation")
                        .content(str)
                        .contentType(MediaType.APPLICATION_JSON)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }

    @Test
    public void getInvitationTest() throws Exception {
        Invitation invitation = new Invitation(26, 75, "No", 8, "r", 3, "esben");
        List<Invitation> invitationList = new ArrayList<>();
        invitationList.add(invitation);
        String str = gson.toJson(invitationList);
        this.mockMvc.perform(MockMvcRequestBuilders
                        .get("/invitation/{id}", 8)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk())
                .andExpect(content().json(str));
    }

    @Test
    public void deleteInvitationTest() throws Exception {
        this.mockMvc.perform(MockMvcRequestBuilders
                        .delete("/invitation/{id}", 27)
                        .accept(MediaType.APPLICATION_JSON))
                .andDo(print())
                .andExpect(status().isOk());
    }


}


