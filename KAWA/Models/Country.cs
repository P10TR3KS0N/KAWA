using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Dodaj to using

namespace CoffeeAppMvc5.Models
{
    public class Country
    {
        [Key] // Wymagane, aby jasno określić klucz główny
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kraju jest wymagana.")] // Walidacja: pole wymagane
        [StringLength(100, ErrorMessage = "Nazwa kraju nie może być dłuższa niż 100 znaków.")] // Walidacja: długość
        [Display(Name = "Nazwa Kraju")] // Etykieta wyświetlana w UI
        public string Name { get; set; }

        // Właściwości nawigacyjne (reprezentują relacje 1-do-wielu)
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<CoffeeBean> CoffeeBeans { get; set; }

        public Country() // Konstruktor, inicjalizuje kolekcje
        {
            Cities = new HashSet<City>();
            CoffeeBeans = new HashSet<CoffeeBean>();
        }
    }
}