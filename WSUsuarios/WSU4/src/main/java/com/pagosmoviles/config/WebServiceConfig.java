package com.pagosmoviles.config;

import org.springframework.boot.web.servlet.ServletRegistrationBean;
import org.springframework.context.ApplicationContext;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.io.ClassPathResource;
import org.springframework.ws.config.annotation.EnableWs;
import org.springframework.ws.config.annotation.WsConfigurerAdapter;
import org.springframework.ws.transport.http.MessageDispatcherServlet;
import org.springframework.ws.wsdl.wsdl11.DefaultWsdl11Definition;
import org.springframework.ws.wsdl.wsdl11.Wsdl11Definition;
import org.springframework.xml.xsd.SimpleXsdSchema;
import org.springframework.xml.xsd.XsdSchema;

@EnableWs
@Configuration
public class WebServiceConfig extends WsConfigurerAdapter {

    @Bean
    public ServletRegistrationBean<MessageDispatcherServlet> messageDispatcherServlet(ApplicationContext context) {
        MessageDispatcherServlet servlet = new MessageDispatcherServlet();
        servlet.setApplicationContext(context);
        servlet.setTransformWsdlLocations(true);
        return new ServletRegistrationBean<>(servlet, "/wsusuarios/*");
    }




    @Bean(name = "wsu04")
    public Wsdl11Definition wsu04WsdlDefinition(XsdSchema transaccionSchema) {
        DefaultWsdl11Definition wsdl11Definition = new DefaultWsdl11Definition();
        wsdl11Definition.setPortTypeName("TransaccionPort");
        wsdl11Definition.setLocationUri("/wsusuarios");
        wsdl11Definition.setTargetNamespace("http://pagosmoviles.com/wsusuarios");
        wsdl11Definition.setSchema(transaccionSchema);
        return wsdl11Definition;
    }






    @Bean
    public XsdSchema transaccionSchema() {
        return new SimpleXsdSchema(new ClassPathResource("xsd/transaccion.xsd"));
    }


}
