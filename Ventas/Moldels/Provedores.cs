﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Ventas.Moldels
{
    public partial class Provedores
    {
        public Provedores()
        {
            Productos = new HashSet<Productos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoEmpresa { get; set; }

        public virtual ICollection<Productos> Productos { get; set; }
    }
}