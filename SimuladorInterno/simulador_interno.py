import tkinter as tk

def enviar_pago():
    # Lógica para enviar pago
    pass

# Crear ventana
ventana = tk.Tk()
ventana.title("Simulador interno")

# Campos de entrada
tk.Label(ventana, text="Teléfono").pack()
telefono_entry = tk.Entry(ventana)
telefono_entry.pack()

tk.Label(ventana, text="Monto").pack()
monto_entry = tk.Entry(ventana)
monto_entry.pack()

tk.Label(ventana, text="Descripción").pack()
descripcion_entry = tk.Entry(ventana)
descripcion_entry.pack()

# Botón enviar
enviar_btn = tk.Button(ventana, text="Enviar", command=enviar_pago)
enviar_btn.pack()

# Ejecutar ventana
ventana.mainloop()
