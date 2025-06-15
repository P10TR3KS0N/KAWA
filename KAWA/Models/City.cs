using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Dodaj to using dla [ForeignKey]

namespace CoffeeAppMvc5.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa miasta jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa miasta nie może być dłuższa niż 100 znaków.")]
        [Display(Name = "Nazwa Miasta")]
        public string Name { get; set; }

        // Klucz obcy do tabeli Country
        [Display(Name = "Kraj")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")] // Opcjonalne, ale dobra praktyka dla jasności
        public virtual Country Country { get; set; } // Właściwość nawigacyjna

        // Właściwość nawigacyjna do powiązanych palarni
        public virtual ICollection<Roaster> Roasters { get; set; }

        public City()
        {
            Roasters = new HashSet<Roaster>();
        }
    }
}