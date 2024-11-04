package com.pagosmoviles;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.boot.jdbc.DataSourceBuilder;

import javax.sql.DataSource;

@SpringBootApplication
public class Main {

    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }

    @Bean
    public DataSource dataSource() {
        DataSourceBuilder<?> dataSourceBuilder = DataSourceBuilder.create();
        dataSourceBuilder.driverClassName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
        dataSourceBuilder.url("jdbc:sqlserver://DESKTOP-L5QPGSA;databaseName=PagosMovilesUsuarios;encrypt=true;trustServerCertificate=true");
        dataSourceBuilder.username("sa");
        dataSourceBuilder.password("root");
        return dataSourceBuilder.build();
    }
}
