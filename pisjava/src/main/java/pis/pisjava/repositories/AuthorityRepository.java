package pis.pisjava.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pis.pisjava.entities.Authority;

@Repository
public interface AuthorityRepository  extends JpaRepository<Authority, Integer> {
}
