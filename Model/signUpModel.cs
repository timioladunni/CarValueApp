using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Model
{
    public class signUpModel
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
