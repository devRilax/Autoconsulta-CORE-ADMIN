﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totem.VO
{
    public class UsuarioAlumno
    {

        public string run { get; set; }
        public string clave { get; set; }
        public int estadoLogin { get; set; }

        public UsuarioAlumno()
        {
        }

        public UsuarioAlumno(string run, string clave)
        {
            this.run = run;
            this.clave = clave;
        }
    }
}
