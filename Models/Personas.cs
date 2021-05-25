using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Registro_Prestamos.Models
{
    public class Personas
    {
        [Key]

        public int PersonaId { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacío")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacío")]
        [RegularExpression("^\\d{3}\\D?\\d{7}\\D?\\d$", ErrorMessage = "Por favor ingrese un No. de Cédula válido.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacío")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Por favor ingrese un No. de Teléfono válido.")]
        public string Telefono { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd,mm,yyyy}")]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public double Balance { get; set; }
    }
}
