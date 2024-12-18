package com.pagosmoviles.config;

import javax.sql.DataSource;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.jdbc.core.JdbcTemplate;

@Configuration
public class JdbcConfig {

    @Bean(name = "usuariosJdbcTemplate")
    public JdbcTemplate usuariosJdbcTemplate(@Qualifier("usuariosDataSource") DataSource dataSource) {
        return new JdbcTemplate(dataSource);
    }

    @Bean(name = "bancoJdbcTemplate")
    public JdbcTemplate bancoJdbcTemplate(@Qualifier("bancoDataSource") DataSource dataSource) {
        return new JdbcTemplate(dataSource);
    }
}
