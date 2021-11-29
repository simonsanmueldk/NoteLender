package PersistenceServer;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@SpringBootApplication
public class PersistenceServer {

    public static void main(String[] args) {
        SpringApplication.run(PersistenceServer.class, args);
    }


}