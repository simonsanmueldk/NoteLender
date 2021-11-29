package PersistenceService;

import DatabaseModels.GroupModel;
import JpaRepositories.GroupRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class GroupService {

    @Autowired
    private GroupRepository groupRepository;

    public List<GroupModel> getAllGroups(){
        return groupRepository.findAll();
    }

}
