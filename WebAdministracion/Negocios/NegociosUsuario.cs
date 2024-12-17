<<<<<<< HEAD
﻿using System;
=======
﻿using Datos;
using Modelos;
using System;
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
<<<<<<< HEAD
    internal class NegociosUsuario
    {
=======
    public class NegociosUsuario
    {
        private readonly DatosUsuario _datosUsuario;

        public NegociosUsuario()
        {
            _datosUsuario = new DatosUsuario();
        }

        public List<ModeloUsuario> ObtenerTodosLosUsuarios()
        {
            //dvuelve la lista de usuarios
            return _datosUsuario.ObtenerUsuarios();
        }

        public void CrearUsuario(ModeloUsuario usuario, string contrasena)
        {
            if (string.IsNullOrEmpty(usuario.Identificacion) ||
                string.IsNullOrEmpty(usuario.NombreUsuario) ||
                string.IsNullOrEmpty(usuario.NombreCompleto) ||
                string.IsNullOrEmpty(usuario.Telefono) ||
                string.IsNullOrEmpty(contrasena))
            {
                throw new Exception("Todos los campos son requeridos.");
            }

            //insert de capa de datos
            _datosUsuario.InsertarUsuario(usuario, contrasena);
        }

        public ModeloUsuario ObtenerUsuarioIdentificacion(string identificacion)
        {
            return _datosUsuario.ObtenerUsuarioPorIdentificacion(identificacion);
        }

        public void ActualizarUsuario(ModeloUsuario usuario, string contrasena)
        {
            _datosUsuario.ActualizarUsuario(usuario, contrasena);
        }

        public void EliminarUsuario(string identificacion)
        {
            if (string.IsNullOrEmpty(identificacion))
            {
                throw new Exception("La identificación no puede estar vacía.");
            }

            _datosUsuario.EliminarUsuario(identificacion);
        }
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    }
}
