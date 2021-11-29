package DatabaseModels;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;

@Entity(name="groups")
public class GroupModel {
    @Id
    private int id;
    @Column(name="groupname")
    private String name;
}
