using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Dto
{
    public class CarValueAppCreateDto
    {
        [Key]
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }

        public int YearOfPurchase { get; set; }
        public int Mileage { get; set; }
       
        public string PlateNumber { get; set; }
 
    }
}
