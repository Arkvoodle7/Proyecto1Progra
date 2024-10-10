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
            respuesta = s.recv(1024).decode('utf-8')
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
            respuesta = s.recv(1024).decode('utf-8')
            print(f"Respuesta del servidor: {respuesta}")
    except Exception as e:
        print(f"Error al conectar con el servidor: {e}")

def enviar_pago(telefono_envia, telefono_recibe, monto, descripcion):
    trama = f"""
    <transaccion>
    <telefono>{telefono_envia}</telefono>
    <monto>{monto}</monto>
    <descripcion>{descripcion}</descripcion>
    </transaccion>
    """
    
    # conexion con el orquestador
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', 8080))  
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(4096).decode('utf-8')
            print(f"Respuesta del servidor: {respuesta}")
    except Exception as e:
        print(f"Error al conectar con el servidor: {e}")

def consultar_saldo(telefono):
    trama = f"""
    <saldo>
    <telefono>{telefono}</telefono>
    </saldo>
    """
    
    # conexion con orquestador
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

def abrir_enviar_pago():
    ventana_pago = tk.Toplevel()
    ventana_pago.title("Enviar pago")
    
    # Campos para enviar pago
    tk.Label(ventana_pago, text="Teléfono del que envía").pack()
    telefono_envia_entry = tk.Entry(ventana_pago)
    telefono_envia_entry.pack()
    
    tk.Label(ventana_pago, text="Teléfono del que recibe").pack()
    telefono_recibe_entry = tk.Entry(ventana_pago)
    telefono_recibe_entry.pack()
    
    tk.Label(ventana_pago, text="Monto").pack()
    monto_entry = tk.Entry(ventana_pago)
    monto_entry.pack()
    
    tk.Label(ventana_pago, text="Descripción").pack()
    descripcion_entry = tk.Entry(ventana_pago)
    descripcion_entry.pack()
    
    enviar_btn = tk.Button(ventana_pago, text="Enviar", command=lambda: enviar_pago(telefono_envia_entry.get(), telefono_recibe_entry.get(), monto_entry.get(), descripcion_entry.get()))  # Aquí va la lógica
    enviar_btn = tk.Button(ventana_pago, text="Enviar", command=lambda: None)  # Aquí va la lógica
    enviar_btn.pack()

def abrir_consultar_saldo():
    ventana_saldo = tk.Toplevel()
    ventana_saldo.title("Consultar saldo")
    
    # Campo y botón para consultar saldo
    tk.Label(ventana_saldo, text="Número de teléfono").pack()
    telefono_saldo_entry = tk.Entry(ventana_saldo)
    telefono_saldo_entry.pack()
    
    consultar_btn = tk.Button(ventana_saldo, text="Consultar", command=lambda: consultar_saldo(telefono_saldo_entry.get()))  # Aquí va la lógica
    consultar_btn.pack()

    consultar_btn = tk.Button(ventana_saldo, text="Consultar", command=lambda: None)  # Aquí va la lógica
    consultar_btn.pack()

# Crear ventana principal
ventana_principal = tk.Tk()
ventana_principal.title("Simulador Interno")
ventana_principal.geometry('400x300')

# Botones en la ventana principal
inscribir_btn = tk.Button(ventana_principal, text="Inscribir/Desinscribir", command=abrir_inscribir_desinscribir)
inscribir_btn.pack()

# Aquí puedes agregar más funcionalidades
enviar_pago_btn = tk.Button(ventana_principal, text="Enviar pago", command=abrir_enviar_pago)
enviar_pago_btn.pack()

consultar_saldo_btn = tk.Button(ventana_principal, text="Consultar saldo", command=abrir_consultar_saldo)
consultar_saldo_btn.pack()

ventana_principal.mainloop()