package pis.pisjava.dtos;

import lombok.Data;

import java.sql.Timestamp;

@Data
public class UserDto {
    private int id;
    private String username;
    private String password;
    private Timestamp create_time;
    private String authority;

    public UserDto() {
    }
}
