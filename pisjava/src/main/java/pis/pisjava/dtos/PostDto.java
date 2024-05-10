package pis.pisjava.dtos;

import jakarta.persistence.Column;
import jakarta.persistence.FetchType;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import pis.pisjava.entities.Category;
import pis.pisjava.entities.User;

import java.sql.Timestamp;

@Data
public class PostDto {
    private int id;
    private String header;
    private String content;
    private Timestamp create_time;
    private UserDto creator;
    private CategoryDto category;

    public PostDto(){

    }

    public PostDto(String message) {
    }
}
