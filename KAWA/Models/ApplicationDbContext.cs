using System.Data.Entity; // WAŻNE: To jest dla Entity Framework 6 na .NET Framework
using CoffeeAppMvc5.Models; // Upewnij się, że używasz poprawnej przestrzeni nazw dla swoich modeli

namespace CoffeeAppMvc5.Models // Możesz zmienić na CoffeeAppMvc5.Data jeśli stworzyłeś folder Data
{
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor, który przekazuje nazwę connection stringa do bazy bazowej DbContext
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        // DbSet dla każdej tabeli w bazie danych
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CoffeeBean> CoffeeBeans { get; set; }
        public DbSet<Roaster> Roasters { get; set; }
        public DbSet<Coffee> Coffees { get; set; }

        // Metoda do konfiguracji modelu, jeśli potrzebne są niestandardowe mapowania
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // W przypadku prostych relacji 1-wiele z nazwanymi kluczami obcymi (np. CountryId),
            // Entity Framework 6 zazwyczaj automatycznie wykrywa relacje.
            // Jawne konfiguracje są potrzebne, gdy konwencje nie są spełnione
            // lub gdy chcesz skonfigurować bardziej złożone relacje (np. wiele-do-wielu).

            // Przykładowe jawne konfiguracje (możesz je usunąć, jeśli EF działa dobrze bez nich,
            // ale dla pewności i jasności są przydatne):

            // Relacja Country (1) <-> (*) City
            modelBuilder.Entity<City>()
                .HasRequired(c => c.Country) // Miasto *musi* mieć kraj
                .WithMany(co => co.Cities) // Kraj ma wiele miast
                .HasForeignKey(c => c.CountryId) // Klucz obcy w City
                .WillCascadeOnDelete(true); // Opcjonalnie: usunięcie kraju usunie powiązane miasta

            // Relacja Country (1) <-> (*) CoffeeBean
            modelBuilder.Entity<CoffeeBean>()
                .HasRequired(cb => cb.OriginCountry)
                .WithMany(co => co.CoffeeBeans)
                .HasForeignKey(cb => cb.OriginCountryId)
                .WillCascadeOnDelete(true);

            // Relacja City (1) <-> (*) Roaster
            modelBuilder.Entity<Roaster>()
                .HasRequired(r => r.City)
                .WithMany(c => c.Roasters)
                .HasForeignKey(r => r.CityId)
                .WillCascadeOnDelete(true);

            // Relacja CoffeeBean (1) <-> (*) Coffee
            modelBuilder.Entity<Coffee>()
                .HasRequired(c => c.Bean)
                .WithMany(cb => cb.Coffees)
                .HasForeignKey(c => c.BeanId)
                .WillCascadeOnDelete(false); // Opcjonalnie: nie kaskadowe usuwanie, jeśli ziarno jest używane w wielu kawach

            // Relacja Roaster (1) <-> (*) Coffee
            modelBuilder.Entity<Coffee>()
                .HasRequired(c => c.Roaster)
                .WithMany(r => r.Coffees)
                .HasForeignKey(c => c.RoasterId)
                .WillCascadeOnDelete(false); // Opcjonalnie: nie kaskadowe usuwanie

            base.OnModelCreating(modelBuilder);
        }
    }
}