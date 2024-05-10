package pis.pisjava.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pis.pisjava.entities.Post;

@Repository
public interface PostRepository extends JpaRepository<Post, Integer> {
}
