plugins {
	java
	id("org.springframework.boot") version "3.3.5"
	id("io.spring.dependency-management") version "1.1.6"
}

group = "com.pagosmoviles"
version = "0.0.1-SNAPSHOT"

java {
	toolchain {
		languageVersion = JavaLanguageVersion.of(17)
	}
}

repositories {
	mavenCentral()
}

dependencies {
	implementation("org.springframework.boot:spring-boot-starter-jdbc")
	implementation("org.springframework.boot:spring-boot-starter-web-services")
	developmentOnly("org.springframework.boot:spring-boot-devtools")
	runtimeOnly("com.microsoft.sqlserver:mssql-jdbc")
	testImplementation("org.springframework.boot:spring-boot-starter-test")
	testRuntimeOnly("org.junit.platform:junit-platform-launcher")
	implementation("wsdl4j:wsdl4j:1.6.3")
	implementation("jakarta.xml.ws:jakarta.xml.ws-api:4.0.2")
	implementation("jakarta.xml.bind:jakarta.xml.bind-api:4.0.2")
	implementation("org.glassfish.jaxb:jaxb-runtime:4.0.2")
	implementation("jakarta.activation:jakarta.activation-api:2.1.3")
}

tasks.withType<Test> {
	useJUnitPlatform()
}
