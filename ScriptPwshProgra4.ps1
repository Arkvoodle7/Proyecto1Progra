# ReceptorExterno
Start-Process PowerShell -ArgumentList 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra\receptor_externo"; dotnet run'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# SocketExterno
Start-Process PowerShell -ArgumentList 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra\socket_externo"; dotnet run'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Recompilar el proyecto Bancario
Start-Process cmd.exe -ArgumentList '/c', 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra" && javac -cp "C:\Users\dylan\Downloads\sqljdbc_12.8.1.0_esn\sqljdbc_12.8\esn\jars\mssql-jdbc-12.8.1.jre11.jar" bancario/App/src/config/*.java bancario/App/src/dto/*.java bancario/App/src/entities/*.java bancario/App/src/interfaces/*.java bancario/App/src/repository/*.java bancario/App/src/services/*.java bancario/App/src/sockets/*.java bancario/App/src/Main.java'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Ejecutar Bancario
Start-Process cmd.exe -ArgumentList '/c', 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra" && java -cp "C:\Users\dylan\Downloads\sqljdbc_12.8.1.0_esn\sqljdbc_12.8\esn\jars\mssql-jdbc-12.8.1.jre11.jar;bancario/App/src" Main'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Orquestador
Start-Process PowerShell -ArgumentList 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra\orquestador"; python orquestador.py'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# SimuladorOtroBanco
Start-Process PowerShell -ArgumentList 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra\SimuladorOtroBanco"; dotnet run'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos

# Simulador Interno
Start-Process PowerShell -ArgumentList 'cd "C:\Users\dylan\Personal\Codes\Proyecto1Progra\SimuladorInterno"; python simulador_interno.py'
Start-Sleep -Seconds 2  # Pausa de 2 segundos para evitar conflictos
