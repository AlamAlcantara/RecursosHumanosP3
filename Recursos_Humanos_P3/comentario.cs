//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recursos_Humanos_P3
{
    using System;
    using System.Collections.Generic;
    
    public partial class comentario
    {
        public int id { get; set; }
        public int id_vacacion { get; set; }
        public string comentario1 { get; set; }
    
        public virtual vacacion vacacion { get; set; }
    }
}
