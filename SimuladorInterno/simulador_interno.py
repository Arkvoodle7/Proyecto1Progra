import socket
import tkinter as tk
import configparser

#funcion para inscribir usuario
def inscribir_usuario(cuenta, identificacion, telefono):
    #leer el puerto del Orquestador desde Config.ini
    config = configparser.ConfigParser()
    config.read('C:/Users/alexl/source/repos/Proyecto1Progra/SimuladorInterno/Config.ini')
    puerto_orquestador = int(config['Orquestador']['puerto'])
    
    #formato de la trama en XML
    trama = f"<inscripcion><cuenta>{cuenta}</cuenta><identificacion>{identificacion}</identificacion><telefono>{telefono}</telefono></inscripcion>"
    
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', puerto_orquestador))
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(1024).decode('utf-8')
            print(f"Respuesta recibida del Orquestador: {respuesta}")
    except Exception as e:
        print(e)

#funcion para desinscribir usuario
def desinscribir_usuario(cuenta, identificacion, telefono):
    #leer el puerto del Orquestador desde Config.ini
    config = configparser.ConfigParser()
    config.read('Config.ini')
    puerto_orquestador = int(config['Orquestador']['puerto'])
    
    #formato de la trama en XML
    trama = f"<desinscripcion><cuenta>{cuenta}</cuenta><identificacion>{identificacion}</identificacion><telefono>{telefono}</telefono></desinscripcion>"
    
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', puerto_orquestador))
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(1024).decode('utf-8')
            print(f"Respuesta recibida del Orquestador: {respuesta}")
    except Exception as e:
        print(e)

def enviar_pago(telefono, monto, descripcion):
    #leer el puerto del Orquestador desde Config.ini
    config = configparser.ConfigParser()
    config.read('Config.ini')
    puerto_orquestador = int(config['Orquestador']['puerto'])
    
    #crear la trama en formato XML
    trama = f"""<transaccion>
                    <telefono>{telefono}</telefono>
                    <monto>{monto}</monto>
                    <descripcion>{descripcion}</descripcion>
                </transaccion>"""
    
    try:
        orquestador_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        orquestador_socket.connect(('localhost', puerto_orquestador))
        orquestador_socket.send(trama.encode('utf-8'))
        
        respuesta = orquestador_socket.recv(1024).decode('utf-8')
        orquestador_socket.close()
        
        mostrar_respuesta(respuesta)
    except socket.error as e:
        mostrar_respuesta(f"Error de conexión con el Orquestador: {str(e)}")

def mostrar_respuesta(respuesta):
    #mostrar la respuesta en la consola
    print(f"Respuesta recibida del Orquestador: {respuesta}")

def consultar_saldo(telefono):
    #leer el puerto del Orquestador desde Config.ini
    config = configparser.ConfigParser()
    config.read('Config.ini')
    puerto_orquestador = int(config['Orquestador']['puerto'])
    
    trama = f"""<saldo>
    <telefono>{telefono}</telefono>
    </saldo>"""
    
    try:
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
            s.connect(('localhost', puerto_orquestador))
            s.sendall(trama.encode('utf-8'))
            respuesta = s.recv(4096).decode('utf-8')
            print(f"Respuesta recibida del Orquestador: {respuesta}")
    except Exception as e:
        print(e)

#funcion para abrir la ventana de inscripcion/desinscripcion
def abrir_inscribir_desinscribir():
    ventana_inscribir = tk.Toplevel()
    ventana_inscribir.title("Inscribir/Desinscribir")
    ventana_inscribir.geometry('300x200')
    
    #campos y botones para inscribir/desinscribir
    tk.Label(ventana_inscribir, text="Identificación").pack()  
    identificacion_entry = tk.Entry(ventana_inscribir)
    identificacion_entry.pack()

    tk.Label(ventana_inscribir, text="Número de cuenta").pack()
    cuenta_entry = tk.Entry(ventana_inscribir)
    cuenta_entry.pack()
    
    tk.Label(ventana_inscribir, text="Teléfono").pack()
    telefono_entry = tk.Entry(ventana_inscribir)
    telefono_entry.pack()
    
    inscribir_btn = tk.Button(ventana_inscribir, text="Inscribir", command=lambda: inscribir_usuario(cuenta_entry.get(), identificacion_entry.get(), telefono_entry.get()))
    inscribir_btn.pack()
    
    desinscribir_btn = tk.Button(ventana_inscribir, text="Desinscribir",command=lambda: desinscribir_usuario(cuenta_entry.get(), identificacion_entry.get(), telefono_entry.get()))
    desinscribir_btn.pack()

def abrir_enviar_pago():
    ventana_pago = tk.Toplevel()
    ventana_pago.title("Enviar pago")
    ventana_pago.geometry('300x200')
    
    #campos para enviar pago
    tk.Label(ventana_pago, text="Teléfono").pack()
    telefono_entry = tk.Entry(ventana_pago)
    telefono_entry.pack()
    
    tk.Label(ventana_pago, text="Monto").pack()
    monto_entry = tk.Entry(ventana_pago)
    monto_entry.pack()
    
    tk.Label(ventana_pago, text="Descripción").pack()
    descripcion_entry = tk.Entry(ventana_pago)
    descripcion_entry.pack()
    
    enviar_btn = tk.Button(ventana_pago, text="Enviar", command=lambda: enviar_pago(telefono_entry.get(), monto_entry.get(), descripcion_entry.get()))
    enviar_btn.pack()

def abrir_consultar_saldo():
    ventana_saldo = tk.Toplevel()
    ventana_saldo.title("Consultar saldo")
    ventana_saldo.geometry('300x200')
    
    #campo y boton para consultar saldo
    tk.Label(ventana_saldo, text="Teléfono").pack()
    telefono_saldo_entry = tk.Entry(ventana_saldo)
    telefono_saldo_entry.pack()
    
    consultar_btn = tk.Button(ventana_saldo, text="Consultar", command=lambda: consultar_saldo(telefono_saldo_entry.get()))
    consultar_btn.pack()

#crear ventana principal
ventana_principal = tk.Tk()
ventana_principal.title("Simulador Interno")
ventana_principal.geometry('300x200')

#botones en la ventana principal
inscribir_btn = tk.Button(ventana_principal, text="Inscribir/Desinscribir", command=abrir_inscribir_desinscribir)
inscribir_btn.pack()

enviar_pago_btn = tk.Button(ventana_principal, text="Enviar pago", command=abrir_enviar_pago)
enviar_pago_btn.pack()

consultar_saldo_btn = tk.Button(ventana_principal, text="Consultar saldo", command=abrir_consultar_saldo)
consultar_saldo_btn.pack()

ventana_principal.mainloop()
