using CarValueApi.Dto;
using CarValueApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Data
{
    public interface ICarValueAppRepo
    {
        IEnumerable<Command> GetAllCommands();

       
        Command GetCommandByID(int id);
        void CreateCommand(Command commandModel);
        Command RunCommand(Root root);
        string BuyCar(BuyCar buyCar);
        public bool SaveChanges();
        signUpModel SignUp(signUpDTO signUp);
        signUpModel LogIn(LoginDTO login);
    }
    
    public interface IRepository
    {

    }
}
