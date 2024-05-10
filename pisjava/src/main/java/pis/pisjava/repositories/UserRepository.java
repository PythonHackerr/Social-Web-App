package pis.pisjava.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pis.pisjava.entities.User;

@Repository
public interface UserRepository  extends JpaRepository<User, Integer> {
}
