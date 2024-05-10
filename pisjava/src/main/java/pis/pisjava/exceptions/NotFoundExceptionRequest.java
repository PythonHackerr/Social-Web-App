package pis.pisjava.exceptions;


import lombok.Data;
import org.springframework.http.HttpStatus;

@Data
public class NotFoundExceptionRequest extends Exception{

    private HttpStatus status = HttpStatus.NOT_FOUND;

    public NotFoundExceptionRequest(String message) {
        super(message);
    }
}
