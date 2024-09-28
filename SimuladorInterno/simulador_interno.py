import tkinter as tk

# Funciones de las ventanas
def abrir_inscribir_desinscribir():
    ventana_inscribir = tk.Toplevel()
    ventana_inscribir.title("Inscribir/Desinscribir")
    
    # Campos y botones para inscribir/desinscribir
    tk.Label(ventana_inscribir, text="Número de cuenta").pack()
    cuenta_entry = tk.Entry(ventana_inscribir)
    cuenta_entry.pack()
    
    tk.Label(ventana_inscribir, text="Número de teléfono").pack()
    telefono_entry = tk.Entry(ventana_inscribir)
    telefono_entry.pack()
    
    inscribir_btn = tk.Button(ventana_inscribir, text="Inscribir", command=lambda: None)  # Aquí va la lógica
    inscribir_btn.pack()
    
    desinscribir_btn = tk.Button(ventana_inscribir, text="Desinscribir", command=lambda: None)  # Aquí va la lógica
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
    
    enviar_btn = tk.Button(ventana_pago, text="Enviar", command=lambda: None)  # Aquí va la lógica
    enviar_btn.pack()

def abrir_consultar_saldo():
    ventana_saldo = tk.Toplevel()
    ventana_saldo.title("Consultar saldo")
    
    # Campo y botón para consultar saldo
    tk.Label(ventana_saldo, text="Número de teléfono").pack()
    telefono_saldo_entry = tk.Entry(ventana_saldo)
    telefono_saldo_entry.pack()
    
    consultar_btn = tk.Button(ventana_saldo, text="Consultar", command=lambda: None)  # Aquí va la lógica
    consultar_btn.pack()

# Crear ventana principal
ventana_principal = tk.Tk()
ventana_principal.title("Simulador Interno")

# Botones en la ventana principal
inscribir_btn = tk.Button(ventana_principal, text="Inscribir/Desinscribir", command=abrir_inscribir_desinscribir)
inscribir_btn.pack()

enviar_pago_btn = tk.Button(ventana_principal, text="Enviar pago", command=abrir_enviar_pago)
enviar_pago_btn.pack()

consultar_saldo_btn = tk.Button(ventana_principal, text="Consultar saldo", command=abrir_consultar_saldo)
consultar_saldo_btn.pack()

# Ejecutar ventana principal
ventana_principal.mainloop()
