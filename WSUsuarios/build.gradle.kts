plugins {
    id("java")

}

java {
    sourceCompatibility = JavaVersion.VERSION_17
    targetCompatibility = JavaVersion.VERSION_17
}

group = "com.pagosmoviles"
version = "1.0-SNAPSHOT"

repositories {
    mavenCentral()

}

dependencies {
    implementation("org.springframework.boot:spring-boot-starter-web-services:3.3.5")
    implementation("com.microsoft.sqlserver:mssql-jdbc:12.8.1.jre11")
    implementation("org.bouncycastle:bcprov-jdk15on:1.70")
    implementation("jakarta.xml.bind:jakarta.xml.bind-api:3.0.1") // Dependencias para JAXB
    implementation("org.glassfish.jaxb:jaxb-runtime:3.0.1") // Dependencias para JAXB
    testImplementation(platform("org.junit:junit-bom:5.9.1"))
    testImplementation("org.junit.jupiter:junit-jupiter")
}



// Tarea para generar XSD a partir de las clases Java
tasks.register<JavaExec>("generateXsd") {
    group = "build"
    description = "Genera XSD a partir de las clases Java"


    classpath = sourceSets["main"].runtimeClasspath
    mainClass.set("com.sun.tools.xjc.XJCFacade")
    args = listOf(
            "-d", "${layout.buildDirectory.get()}/generated-sources/xjc", // Carpeta temporal para la generación
            "-p", "com.pagosmoviles.generated",
            "-s", "$projectDir/src/main/resources/generated-xsd", // Carpeta donde se generarán los XSD
            "$projectDir/src/main/java/com/pagosmoviles/**/*.java" // Ruta a las clases Java
    )
}

tasks.build {
    dependsOn("generateXsd")
}


tasks.test {
    useJUnitPlatform()
}