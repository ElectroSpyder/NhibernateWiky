using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Entity
{
    public class Empleado
    {
        public virtual Guid Id { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual Almacenamiento Almacenamiento { get; set; }
    }
}
