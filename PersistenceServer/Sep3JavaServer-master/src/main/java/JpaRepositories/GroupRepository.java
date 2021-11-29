package JpaRepositories;

import DatabaseModels.GroupModel;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import org.springframework.stereotype.Service;

import java.util.List;

@Repository
public interface GroupRepository extends JpaRepository<GroupModel, Long> {

    @Override
    List<GroupModel> findAll();
}
