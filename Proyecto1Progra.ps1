# ReceptorExterno
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\receptor_externo"; dotnet run'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# SocketExterno
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\socket_externo"; dotnet run'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Recompilar el proyecto Bancario
Start-Process cmd.exe -ArgumentList '/c', 'cd "C:\Users\alexl\source\repos\Proyecto1Progra" && javac -cp "C:\Program Files\sqljdbc_12.8\enu\jars\mssql-jdbc-12.8.1.jre11.jar" bancario/App/src/config/*.java bancario/App/src/dto/*.java bancario/App/src/entities/*.java bancario/App/src/interfaces/*.java bancario/App/src/repository/*.java bancario/App/src/services/*.java bancario/App/src/sockets/*.java bancario/App/src/Main.java'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Ejecutar Bancario
Start-Process cmd.exe -ArgumentList '/c', 'cd "C:\Users\alexl\source\repos\Proyecto1Progra" && java -cp "C:\Program Files\sqljdbc_12.8\enu\jars\mssql-jdbc-12.8.1.jre11.jar;bancario/App/src" Main'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Orquestador
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\orquestador"; python orquestador.py'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU1
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU1"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU2
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU2"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU3
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU3"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU4
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU4"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU5
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU5"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# WSU6
Start-Process powershell -ArgumentList 'cd "C:\Users\alexl\source\repos\Proyecto1Progra\WSUsuarios\WSU6"; ./gradlew clean build bootRun'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Ejecutar WSA1
Start-Process "C:\Program Files (x86)\IIS Express\iisexpress.exe" -ArgumentList '/site:WSA1 /trace:error'
Start-Sleep -Seconds 2  # Pausa para evitar conflictos
Start-Process "http://localhost:8088/WSA1.asmx"  # Abrir en el navegador

# Ejecutar WSA2
Start-Process "C:\Program Files (x86)\IIS Express\iisexpress.exe" -ArgumentList '/site:WSA2 /trace:error'
Start-Sleep -Seconds 2
Start-Process "http://localhost:8089/WSA2.asmx"

# Ejecutar WebServiceAD_Usuarios
Start-Process "C:\Program Files (x86)\IIS Express\iisexpress.exe" -ArgumentList '/site:WebServiceAD_Usuarios /trace:error'
Start-Sleep -Seconds 2
Start-Process "http://localhost:8090/WebServiceAD_Usuarios.asmx"

# Ejecutar WebServiceAD_Cuentas
Start-Process "C:\Program Files (x86)\IIS Express\iisexpress.exe" -ArgumentList '/site:WebServiceAD_Cuentas /trace:error'
Start-Sleep -Seconds 2
Start-Process "http://localhost:8091/WebServiceAD_Cuentas.asmx"