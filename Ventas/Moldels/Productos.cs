﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ventas.Moldels
{
    public partial class Productos
    {
        public Productos()
        {
            Clientes = new HashSet<Clientes>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public string imagen { get; set; }
        public int? Provedor { get; set; }

        [NotMapped]
        public IFormFile ImagenFile { get; set; }



        public virtual Provedores ProvedorNavigation { get; set; }
        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}