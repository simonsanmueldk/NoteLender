package PersistenceServer;/*
 * 03.10.2021 Original version
 */


import com.google.gson.Gson;
import org.springframework.web.bind.annotation.*;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;


@RestController
public class PersistenceServerController {

    private static final Gson gson = new Gson();
    private final Connection connection =
            DriverManager.getConnection("jdbc:postgresql://localhost:5432/postgres", "postgres", "qwerty");

    public PersistenceServerController() throws SQLException {
    }


    @GetMapping("/Group/{id}")
    public synchronized String getGroup(@PathVariable(value = "id") int id) throws SQLException {
        System.out.println("It's working Get");
        String text = "";
        ResultSet resultSet = connection.createStatement().executeQuery
                ("SELECT * FROM sep3.notes WHERE id = " + id);
        while (resultSet.next()) {
            text = resultSet.getString(2);
            System.out.println(text);
        }
        List<String> AdultsList = new ArrayList<>();
        AdultsList.add(text);
        return gson.toJson(AdultsList);
    }

    @PutMapping("/Group")
    public synchronized String createGroup(@RequestBody String json) {
        System.out.println("It's working Post");
        System.out.println(json);
        try {
            connection.createStatement().execute("INSERT INTO sep3.notes (id,string) VALUES ("
                    + '5' + ",'" + json + "')");
            return getGroup(5);
        } catch (SQLException e) {
            System.out.println("Connection failure.");
            e.printStackTrace();
        }
        return null;
    }
}
