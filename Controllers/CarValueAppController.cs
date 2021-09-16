
using AutoMapper;
using CarValueApi.Data;
using CarValueApi.Dto;
using CarValueApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace CarValueApi.Controllers
{
    [Route("api/carvalueapp")]
    [ApiController]
    [Authorize]
    public class CarValueAppController : ControllerBase
    {
        private ICarValueAppRepo _repository;
        private IMapper _mapper;
        
        public CarValueAppController(ICarValueAppRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }

        //GET api/carvalueapp
        [AllowAnonymous]
        [HttpPost("SignUp")]
        public ActionResult<signUpModel> SignUp([FromBody] signUpDTO sign)
        {
            try
            {
                if (!(sign == null))
                {
                    var sig = _repository.SignUp(sign);
                    return Ok($"Sign Up Successful {sign.Surname} {sign.FirstName}, your password has been sent to your email address");
                } 
            }
            catch (Exception et)
            {
                var a = Convert.ToString(new Exception(et.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult<ResponseDTO> LogIn([FromBody] LoginDTO login)
        {
            try
            {
                if (!(login == null))
                {
                    var log = _repository.LogIn(login);
                    var tokenString = GenerateJSONWebToken(log);
                    ResponseDTO response = new ResponseDTO();
                    response.Name = $"Welcome {log.Surname} {log.FirstName} thanks for loging in";
                    response.Token = $"Here is your token: {tokenString}";
                    return response;
                }
                
            }
            catch (Exception ew)
            {
                var a = Convert.ToString(new Exception(ew.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }
        
        /// <summary>
        ///  This Get method is ude to return all the records in the database
        /// </summary>
        /// <returns></returns>
        
        [HttpGet("View")]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CarValueAppReadDto>>(commandItems));
        }

        /// <summary>
        /// This Get method is called with the particular id in search for
        /// it returns the particular id that matches in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("View/{id}")]
        public ActionResult<Command> GetCommandByid(int id)
        {
            if (id != null)
            {
                var commandItem2 = _repository.GetCommandByID(id);
                return Ok(commandItem2);
            }
            return NoContent();
        }

        

        /// <summary>
        /// This Post method is used to calculate the current car value of the car with the inputed parameters
        /// You need the following post parameters: "make", "model", "year" "yearOfPurchase", "price", "color",
        /// "milage", "plateNumber"
        /// The api returns a new value; the carCurrentPrice.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        [HttpPost("CarCurrentValue")]
        public ActionResult<Command> RunCommand([FromBody] Root root)
        {
            try
            {
                if (!(root == null))
                {
                    var cd = _repository.RunCommand(root);
                    _repository.SaveChanges();
                    return Ok(cd);
                }
            }
            catch (Exception ea)
            {
                var a = Convert.ToString(new Exception( ea.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }

        /// <summary>
        /// This Post method is used to purchase a selected car with the inputed parameters
        /// You need the following post parameters: "car_id", "surname_input", "firstname_input", "phone_number", "email_address",
        /// The API returns the object reference
        /// </summary>
        /// <param name="buyCar"></param>

        /// <returns></returns>
        [HttpPost("BuyCar")]
        public ActionResult BuyCar([FromBody] BuyCar buyCar)
        {
            try
            {
                if (!(buyCar == null))
                {
                    var car = _repository.BuyCar(buyCar);
                    return Ok(buyCar.object_reference);
                }
            }
          
            catch (Exception eq)
            {
                var a = Convert.ToString(new Exception(eq.Message));
                return UnprocessableEntity(a);
            }
            return NoContent();
        }
        [AllowAnonymous]
        [HttpGet("message")]
        public  ActionResult SendMessage()
        {
            
                var mail = new MailMessage();
                string email = "credequityltd@gmail.com";
                string password = "pAssword10!";
                var loginInfo = new NetworkCredential(email, password);
                mail.From = new MailAddress(email);
                mail.To.Add(new MailAddress("timilehinayomipo@yahoo.com"));
                mail.Subject = "TEST";
                mail.Body = "Hello Timi";
                mail.IsBodyHtml = true;
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                return Ok();
        }
        private string GenerateJSONWebToken(signUpModel userInfo)
        {
            var key = ConfigurationManager.AppSettings["Key"];
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audience = ConfigurationManager.AppSettings["Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Surname),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
              issuer,
              audience,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
    
 

    




}

