using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeAppMvc5.Models
{
    public class CoffeeBean
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa ziarna jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa ziarna nie może być dłuższa niż 100 znaków.")]
        [Display(Name = "Nazwa Ziarna")]
        public string Name { get; set; }

        // Klucz obcy do tabeli Country (kraj pochodzenia ziarna)
        [Display(Name = "Kraj Pochodzenia")]
        public int OriginCountryId { get; set; }

        [ForeignKey("OriginCountryId")]
        public virtual Country OriginCountry { get; set; } // Właściwość nawigacyjna

        // Właściwość nawigacyjna do powiązanych kaw
        public virtual ICollection<Coffee> Coffees { get; set; }

        public CoffeeBean()
        {
            Coffees = new HashSet<Coffee>();
        }
    }
}