using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Dto
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string LoginEmail { get; set; }
        [Required]
        public string LoginPassword { get; set; }
    }
}
