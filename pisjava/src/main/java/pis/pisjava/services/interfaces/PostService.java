package pis.pisjava.services.interfaces;

import pis.pisjava.dtos.PostDto;
import pis.pisjava.exceptions.NotFoundExceptionRequest;

import java.util.List;

public interface PostService {
    List<PostDto> getPosts() throws NotFoundExceptionRequest;
    PostDto getPost(int postId) throws NotFoundExceptionRequest;
    PostDto addPost(PostDto postDto) throws NotFoundExceptionRequest;
    void deletePost(int postId) throws NotFoundExceptionRequest;;
}
