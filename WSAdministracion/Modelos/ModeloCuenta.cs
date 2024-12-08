using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ModeloCuenta
    {
        public string NumeroCuenta { get; set; }
        public string NombreUsuario { get; set; }
        public string TipoCuenta { get; set; }
        public decimal Saldo { get; set; }
    }
}
