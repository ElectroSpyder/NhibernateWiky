using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Entity
{
    public class Producto
    {
        //solo NHibernate va a setear el id es por eso el protected de id
        public virtual Guid Id { get; protected set; }
        public virtual string Nombre { get; set; }
        public virtual double Precio { get; set; }
        public virtual IList<Almacenamiento> Almacenamientos { get; set; }

        public Producto()
        {
            Almacenamientos = new List<Almacenamiento>();
        }
    }
}
