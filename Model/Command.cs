using CarValueApi.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Model
{
    
    public class Command 
    {
       /// <summary>
       /// This property is to get the Make of the car
       /// </summary>
        public string Make { get; set; }
        /// <summary>
        ///  This property is to get the Model of the car
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        ///  This property is to get the Year the Car was manufactured
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        ///  This property is to get the Color of the Car
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        ///  This property is to get the Price of the car 
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        ///  This property is to get the Year the car was purchased
        /// </summary>
        public int YearOfPurchase { get; set; }
        /// <summary>
        ///  This property is to get the total milage the car has moved
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// The Id as automatically generated from the database and it is a primary key
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// This is the car's plate number, it a unique identifier 
        /// </summary>
        public string PlateNumber { get; set; }
        /// <summary>
        /// The Car current price is automtically generated using the values from the year,year of purchase and milage
        /// It is stored as a decimal
        /// </summary>

        public decimal CarCurrentPrice { get; set; }
        


    }
}
