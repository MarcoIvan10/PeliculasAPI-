using System.ComponentModel.DataAnnotations;
using PeliculasAPI.Validaciones;

namespace PeliculasAPI.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")] // regla de validación de ASP.NET
        [StringLength(50, ErrorMessage = "El campo {0} debe tener {1} cracteres o menos")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; } // required es una regla de C#

        //[Range(18, 120)]
        //public int Edad { get; set; }

        //[CreditCard]
        //public string? TarjetaDeCredito { get; set; }
        //[Url]
        //public string? Url { get; set; }
    }

}
