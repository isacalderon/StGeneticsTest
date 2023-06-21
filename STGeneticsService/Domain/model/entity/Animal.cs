
namespace STGeneticsService.Domain.Model.Entity
{
    public class Animal {

        public int animalId { get; set; }
        public string? name { get; set; }
        public string? breed { get; set; }
        public DateTime birthDate { get; set; }

        public int sex{ get; set;}
        public double price { get; set; }

        public int status { get; set; }
    }
}