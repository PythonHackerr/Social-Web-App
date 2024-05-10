package pis.pisjava.controllers;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import pis.pisjava.dtos.PostDto;
import pis.pisjava.services.interfaces.PostService;

import java.util.List;

@RestController
@RequestMapping
public class PostController {

    private PostService postService;

    public PostController(PostService postService) {
        this.postService = postService;
    }

    @GetMapping("/post")
    public List<PostDto> getPost(){
        return postService.getPosts();
    }
}
