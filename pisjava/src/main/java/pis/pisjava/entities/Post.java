package pis.pisjava.entities;


import jakarta.persistence.*;
import lombok.*;

import java.sql.Timestamp;


@Entity
@Table(name="post")
@Getter
@Setter
@ToString
@RequiredArgsConstructor
public class Post {

    @Id
    @GeneratedValue(strategy= GenerationType.IDENTITY)
    @Column(name="id")
    private int id;

    @Column(name="header")
    private String header;

    @Column(name="content")
    private String content;

    @Column(name="create_time")
    private Timestamp createTime;

    @ManyToOne(fetch=FetchType.EAGER)
    @JoinColumn(name="creator_id")
    private User creator;

    @ManyToOne(fetch=FetchType.EAGER)
    @JoinColumn(name="category_id")
    private Category category;

    public Post(String header, String content, Timestamp createTime, User creator, Category category) {
        this.header = header;
        this.content = content;
        this.createTime = createTime;
        this.creator = creator;
        this.category = category;
    }
}
