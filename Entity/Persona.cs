﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Entity
{
    public class Persona
    {
        public string Identificacion { get; set; }

        public string Apellido  { get; set; }
        public string Nombre { get; set; }
        public int  Edad { get; set; }
        public char Genero { get; set; }
        public string Email { get; set; }
        public decimal Pulsacion {
            get
            { 
                if (Genero.Equals("F"))
                {
                    return (220 - Edad) / 10;
                }
                else
                {
                    return (210 - Edad) / 10;
                }

            }
        }
    }
}
