using CarValueApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarValueApi.Data;
using CarValueApi.Dto;

namespace CarValueApi.Data
{
    public class MockCarValueAppRepo : ICarValueAppRepo 
    {
        public string BuyCar(BuyCar buyCar)
        {
            throw new NotImplementedException();
        }

        public void CreateCommand(Command commandModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {

                new Command {
                    Make="Honda", 
                    Model="CRV",
                    Year = 2015, 
                    YearOfPurchase=2019, 
                    Mileage=20000, 
                    Color="Blue",
                    Price=10000, 
                    PlateNumber="234dfg34"
                },
                new Command {
                    Make="Honda", 
                    Model="CRV",
                    Year = 2015, 
                    YearOfPurchase=2019,
                    Mileage=20000, 
                    Color="Blue", 
                    Price=10000,
                    PlateNumber="234dfg34"
                },
                new Command {
                    Make="Honda",
                    Model="CRV",
                    Year = 2015, 
                    YearOfPurchase=2019,
                    Mileage=20000, 
                    Color="Blue", 
                    Price=10000, 
                    PlateNumber="234dfg34"
                }

            };
            return commands;
        }

        public Command GetCommandByID(int id)
        {
            throw new NotImplementedException();
        }

        public Command GetCommandByModel(string carModelInput)
        {
            return new Command {
                Make="Honda", 
                Model="CRV",
                Year = 2015, 
                YearOfPurchase=2019, 
                Mileage=20000, 
                Color="Blue", 
                Price=10000, 
                PlateNumber="234dfg34"
            };
        }

        public Command GetCommandByPlateNumber(string carPlateNumberInput)
        {
            return new Command { 
                Make = "Honda", Model = "CRV", 
                Year = 2015, YearOfPurchase = 2019,
                Mileage = 20000, Color = "Blue",
                Price = 10000, PlateNumber = "234dfg34" 
            };

        }

        public signUpModel LogIn(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public Command RunCommand(Root root)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public signUpModel SignUp(signUpModel signUp)
        {
            throw new NotImplementedException();
        }

        public signUpModel SignUp(signUpDTO signUp)
        {
            throw new NotImplementedException();
        }
    }
}
