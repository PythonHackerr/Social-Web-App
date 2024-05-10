package pis.pisjava.entities;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;
import lombok.ToString;

@Entity
@Table(name="authority")
@Getter
@Setter
@ToString
@RequiredArgsConstructor
public class Authority {

    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    @Column(name="id")
    private int id;

    @Column(name="name")
    private String name;

    public Authority(int id, String name) {
        this.id = id;
        this.name = name;
    }
}
