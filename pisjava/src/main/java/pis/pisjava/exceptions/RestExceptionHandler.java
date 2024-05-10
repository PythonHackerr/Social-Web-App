package pis.pisjava.exceptions;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;
import pis.pisjava.exceptions.dtos.NotFoundExceptionRequestDto;

import java.sql.Timestamp;

@ControllerAdvice
public class RestExceptionHandler extends ResponseEntityExceptionHandler {

    @ExceptionHandler(value= {NotFoundExceptionRequest.class})
    protected ResponseEntity<Object> handleEntityNotFound(
            Exception ex){
        NotFoundExceptionRequestDto exception = new NotFoundExceptionRequestDto(
                ex.getMessage(),
                new Timestamp(System.currentTimeMillis()));
        return new ResponseEntity<>(exception,exception.getStatus());
    }
}
