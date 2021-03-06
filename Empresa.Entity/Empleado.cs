﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.Entity
{
    public class Empleado
    {
        //solo NHibernate va a setear el id es por eso el protected de id
        public virtual Guid Id { get; protected set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellido { get; set; }
        public virtual Almacenamiento Almacenamiento { get; set; }
    }
}
