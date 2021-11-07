package com.example.javaserver;/*
 * 03.10.2021 Original version
 */



import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;


@RestController
public class AltTier3Controller {

    private static final Gson gson = new Gson();
    private final Connection connection =
            DriverManager.getConnection("jdbc:postgresql://localhost:5432/postgres", "postgres", "qwerty");
    private final Statement statement = connection.createStatement();

    public AltTier3Controller() throws SQLException {
    }


    @GetMapping("/Group/{Id}")
    public synchronized String getAccount(@PathVariable(value = "Id") int id) throws SQLException {
        System.out.println("It's working");
        List<String> NoteList = new ArrayList<>();
        ResultSet resultSet = statement.executeQuery
                ("SELECT * FROM sep3.notes WHERE id = " + id);
        while (resultSet.next()) {
            NoteList.add(resultSet.getString(2));

        }
        return gson.toJson(NoteList);
    }


}
