using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeAppMvc5.Models
{
    public class Roaster
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa palarni jest wymagana.")]
        [StringLength(150, ErrorMessage = "Nazwa palarni nie może być dłuższa niż 150 znaków.")]
        [Display(Name = "Nazwa Palarni")]
        public string Name { get; set; }

        // Klucz obcy do tabeli City
        [Display(Name = "Miasto Palarni")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; } // Właściwość nawigacyjna

        // Właściwość nawigacyjna do powiązanych kaw
        public virtual ICollection<Coffee> Coffees { get; set; }

        public Roaster()
        {
            Coffees = new HashSet<Coffee>();
        }
    }
}