package pis.pisjava.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import pis.pisjava.entities.Comment;

@Repository
public interface CommentRepository  extends JpaRepository<Comment, Integer> {
}
