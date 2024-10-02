import socket
import tkinter as tk

# Función para inscribir usuario
def inscribir_usuario(cuenta, identificacion, telefono):
    # Formato de la trama en XML
    trama = f"<inscripcion><cuenta>{cuenta}</cuenta><identificacion>{identificacion}</identificacion><telefono>{telefono}</telefono></inscripcion>"
    
    # Conexión al socket orquestador
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', 8080)) 
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(4096).decode('utf-8')
            print(f"Respuesta del servidor: {respuesta}")
    except Exception as e:
        print(f"Error al conectar con el servidor: {e}")

# Función para desinscribir usuario
def desinscribir_usuario(cuenta, identificacion, telefono):
    # Formato de la trama en XML
    trama = f"<desinscripcion><cuenta>{cuenta}</cuenta><identificacion>{identificacion}</identificacion><telefono>{telefono}</telefono></desinscripcion>"
    
    # Conexión al socket orquestador
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', 8080))  
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(4096).decode('utf-8')
            print(f"Respuesta del servidor: {respuesta}")
    except Exception as e:
        print(f"Error al conectar con el servidor: {e}")

# Función para abrir la ventana de inscripción/desinscripción
def abrir_inscribir_desinscribir():
    ventana_inscribir = tk.Toplevel()
    ventana_inscribir.title("Inscribir/Desinscribir")
    
    # Campos y botones para inscribir/desinscribir
    tk.Label(ventana_inscribir, text="Número de cuenta").pack()
    cuenta_entry = tk.Entry(ventana_inscribir)
    cuenta_entry.pack()
    
    tk.Label(ventana_inscribir, text="Identificación").pack()  
    identificacion_entry = tk.Entry(ventana_inscribir)
    identificacion_entry.pack()
    
    tk.Label(ventana_inscribir, text="Número de teléfono").pack()
    telefono_entry = tk.Entry(ventana_inscribir)
    telefono_entry.pack()
    
    inscribir_btn = tk.Button(ventana_inscribir, text="Inscribir", command=lambda: inscribir_usuario(cuenta_entry.get(), identificacion_entry.get(), telefono_entry.get()))
    inscribir_btn.pack()
    
    desinscribir_btn = tk.Button(ventana_inscribir, text="Desinscribir",command=lambda: desinscribir_usuario(cuenta_entry.get(), identificacion_entry.get(), telefono_entry.get()))
    desinscribir_btn.pack()

# Crear ventana principal
ventana_principal = tk.Tk()
ventana_principal.title("Simulador Interno")

# Botones en la ventana principal
inscribir_btn = tk.Button(ventana_principal, text="Inscribir/Desinscribir", command=abrir_inscribir_desinscribir)
inscribir_btn.pack()

# Aquí puedes agregar más funcionalidades
enviar_pago_btn = tk.Button(ventana_principal, text="Enviar pago", command=lambda: None)
enviar_pago_btn.pack()

consultar_saldo_btn = tk.Button(ventana_principal, text="Consultar saldo", command=lambda: None)
consultar_saldo_btn.pack()

# Ejecutar ventana principal
ventana_principal.mainloop()
