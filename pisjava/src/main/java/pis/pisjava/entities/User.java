package pis.pisjava.entities;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;
import lombok.ToString;

import java.sql.Timestamp;
import java.util.Set;

@Entity
@Table(name="user")
@Getter
@Setter
@ToString
@RequiredArgsConstructor
public class User {

    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    @Column(name="id")
    private int id;

    @Column(name="username")
    private String username;

    @Column(name="password")
    private String password;

    @Column(name="email")
    private String email;

    @Column(name="create_time")
    private Timestamp createTime;

    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(name="authority_id")
    private Authority authority;

    public User(int id, String username, String password, String email, Timestamp createTime, Authority authority) {
        this.id = id;
        this.username = username;
        this.password = password;
        this.email = email;
        this.createTime = createTime;
        this.authority = authority;
    }
}
