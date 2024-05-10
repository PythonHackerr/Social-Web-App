package pis.pisjava.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pis.pisjava.entities.Category;

import java.util.Optional;

@Repository
public interface CategoryRepository  extends JpaRepository<Category, Integer> {
    Optional<Category> findByName(String name);
}
