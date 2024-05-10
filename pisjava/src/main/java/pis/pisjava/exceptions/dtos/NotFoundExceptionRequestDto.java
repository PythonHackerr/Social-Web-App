package pis.pisjava.exceptions.dtos;

import lombok.Data;
import org.springframework.http.HttpStatus;

import java.sql.Timestamp;

@Data
public class NotFoundExceptionRequestDto {

    private HttpStatus status = HttpStatus.NOT_FOUND;

    private String message;

    private Timestamp timestamp;

    public NotFoundExceptionRequestDto(String message, Timestamp timestamp) {
        this.message = message;
        this.timestamp = timestamp;
    }
}
