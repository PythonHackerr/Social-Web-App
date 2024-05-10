package pis.pisjava;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.autoconfigure.cassandra.CassandraAutoConfiguration;
import org.springframework.boot.autoconfigure.data.cassandra.CassandraDataAutoConfiguration;

@SpringBootApplication
public class PisjavaApplication {

	public static void main(String[] args) {
		SpringApplication.run(PisjavaApplication.class, args);
	}

}
