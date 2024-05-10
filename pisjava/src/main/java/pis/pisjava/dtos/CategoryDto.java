package pis.pisjava.dtos;

import lombok.Data;

@Data
public class CategoryDto {

    private int id;

    private String name;

    public CategoryDto(String name) {
        this.name = name;
    }
}
