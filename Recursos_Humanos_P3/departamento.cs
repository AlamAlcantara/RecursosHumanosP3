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
    using System.ComponentModel.DataAnnotations;
    
    public partial class departamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public departamento()
        {
            this.empleado = new HashSet<empleado>();
        }
    
        public int id { get; set; }
        [Required(ErrorMessage = "El código del departamento es requerido")]
        public string Codigo_Departamento { get; set; }
        [Required(ErrorMessage = "El nombre del departamento es requerido")]
        public string Nombre { get; set; }
        public Nullable<int> encargado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<empleado> empleado { get; set; }
        public virtual empleado empleado1 { get; set; }
    }
}
