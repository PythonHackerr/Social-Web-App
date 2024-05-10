package pis.pisjava.services;

import org.springframework.stereotype.Service;
import pis.pisjava.dtos.PostDto;
import pis.pisjava.entities.Category;
import pis.pisjava.entities.Post;
import pis.pisjava.entities.User;
import pis.pisjava.exceptions.NotFoundExceptionRequest;
import pis.pisjava.repositories.CategoryRepository;
import pis.pisjava.repositories.PostRepository;
import pis.pisjava.repositories.UserRepository;
import pis.pisjava.services.interfaces.PostService;

import java.sql.Timestamp;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class PostServiceImpl implements PostService {

    private PostRepository postRepository;
    private UserRepository userRepository;
    private CategoryRepository categoryRepository;

    public PostServiceImpl(PostRepository postRepository) {
        this.postRepository = postRepository;
    }


    public List<PostDto> getPosts() {
        List<Post> list = postRepository.findAll();
        List<PostDto> listOfDtos = list.stream().map(post -> new PostDto(post.getContent())).collect(Collectors.toList());
        return listOfDtos;
    }

    @Override
    public PostDto getPost(int postId) throws NotFoundExceptionRequest {
        return null;
    }

    @Override
    public PostDto addPost(PostDto postDto) throws NotFoundExceptionRequest{
        User creator = new User();

        Category category = categoryRepository.findByName(postDto.getCategory().getName())
                .orElseThrow(() -> new NotFoundExceptionRequest("Category with name + "+
                        postDto.getCategory().getName()+" does not exist"));

        Post post = new Post(postDto.getHeader(),postDto.getContent(),
                new Timestamp(System.currentTimeMillis()),creator,category);

        PostDto returnedPostDto = new PostDto();
        returnedPostDto.setId(post.getId());
        returnedPostDto.setHeader(post.getHeader());
        returnedPostDto.setContent(post.getContent());
        returnedPostDto.setCreate_time(post.getCreateTime());
        returnedPostDto.setCreator(null);
        returnedPostDto.setCategory(null);
        return returnedPostDto;
    }

    @Override
    public void deletePost(int postId) {

    }
}
