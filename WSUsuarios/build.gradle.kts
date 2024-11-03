plugins {
    id("java") // No necesita apply false
    id("org.springframework.boot") version "3.3.5" apply false
    id("io.spring.dependency-management") version "1.0.15.RELEASE" apply false
    id("org.unbroken-dome.xjc") version "2.0.0" apply false
    kotlin("jvm") version "1.8.20" apply false
}

allprojects {
    group = "com.pagosmoviles"
    version = "1.0-SNAPSHOT"

    repositories {
        mavenCentral()
    }
}

subprojects {
    apply(plugin = "java")
    apply(plugin = "org.springframework.boot")
    apply(plugin = "io.spring.dependency-management")
    apply(plugin = "kotlin")

    java {
        toolchain {
            languageVersion.set(JavaLanguageVersion.of(17))
        }
    }

    dependencies {
        // Dependencias comunes a todos los módulos
        implementation("org.springframework.boot:spring-boot-starter-data-jdbc")
        implementation("javax.ws.rs:javax.ws.rs-api:2.1")
        implementation("javax.xml.bind:jaxb-api:2.3.1")
        implementation("org.glassfish.jaxb:jaxb-runtime:2.3.1")
        implementation("org.springframework.boot:spring-boot-starter-web-services:3.3.5")
        implementation("org.bouncycastle:bcprov-jdk15on:1.70")
        implementation("jakarta.xml.bind:jakarta.xml.bind-api:3.0.1")
        implementation("org.glassfish.jaxb:jaxb-runtime:3.0.1")
        implementation("org.springframework.boot:spring-boot-starter-web")
        implementation("com.fasterxml.jackson.dataformat:jackson-dataformat-xml")

        // Dependencias para testing
        testImplementation(platform("org.junit:junit-bom:5.9.1"))
        testImplementation("org.junit.jupiter:junit-jupiter")
    }

    tasks.test {
        useJUnitPlatform()
    }
}
