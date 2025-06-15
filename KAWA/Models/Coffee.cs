using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Dla [ForeignKey]

namespace CoffeeAppMvc5.Models
{
    public class Coffee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kawy jest wymagana.")]
        [StringLength(200, ErrorMessage = "Nazwa kawy nie może być dłuższa niż 200 znaków.")]
        [Display(Name = "Nazwa Kawy")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Poziom palenia jest wymagany.")]
        [StringLength(50, ErrorMessage = "Poziom palenia nie może być dłuższy niż 50 znaków.")]
        [Display(Name = "Poziom Palenia")]
        public string RoastLevel { get; set; } // np. "Jasny", "Średni", "Ciemny"

        [Required(ErrorMessage = "Cena jest wymagana.")]
        [Range(0.01, 1000.00, ErrorMessage = "Cena musi być w zakresie od 0.01 do 1000.00.")]
        [DataType(DataType.Currency)] // Sugeruje, że to waluta
        [Display(Name = "Cena (PLN)")]
        public decimal Price { get; set; }

        // Klucz obcy do ziarna kawy
        [Display(Name = "Ziarno Kawy")]
        public int BeanId { get; set; }

        [ForeignKey("BeanId")]
        public virtual CoffeeBean Bean { get; set; } // Właściwość nawigacyjna

        // Klucz obcy do palarni
        [Display(Name = "Palarnia")]
        public int RoasterId { get; set; }

        [ForeignKey("RoasterId")]
        public virtual Roaster Roaster { get; set; } // Właściwość nawigacyjna
    }
}