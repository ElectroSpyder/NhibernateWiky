using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Entity
{
    public class Almacenamiento
    {
        //solo NHibernate va a setear el id es por eso el protected de id
        //virtual porque nhibernate crea proxis y esto 
        //hace que las propiedades esten listas para su sobre escritura
        public virtual Guid Id { get; protected set; }
        public virtual string Nombre { get; set; }
        public virtual IList<Empleado> Empleados { get; protected set; }
        public virtual IList<Producto> Productos { get; protected set; }

        public Almacenamiento()
        {
            Empleados = new List<Empleado>();
            Productos = new List<Producto>();
        }

        //metodos que son usados para agregar items a la coleccion
        //y actuan como las relaciones de las otras clases
        public virtual void AddProducto(Producto producto)
        {
            producto.Almacenamientos.Add(this);
            Productos.Add(producto);
        }

        public virtual void AddEmpleado(Empleado empleado)
        {
            empleado.Almacenamiento = this;
            Empleados.Add(empleado);
        }
    }
}
