using CarValueApi.Dto;
using CarValueApi.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocketLabs.InjectionApi;
using SocketLabs.InjectionApi.Message;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarValueApi.Data
{
    public class SqlCarValueAppRepo : ICarValueAppRepo
    {
        private CarValueAppContext _context;
        public SqlCarValueAppRepo(CarValueAppContext context)
        {
            _context = context;
        }
        public IEnumerable<Command> GetAllCommands()
        {
            return _context.carValueAPI.ToList();
        }

        /// <summary>
        /// worijforijfiorjf nrioje
        /// eoirfheiofepokeprk eporc 
        /// elroj
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Command GetCommandByID(int id)
        {
            return _context.carValueAPI.FirstOrDefault(t => t.ID == id);
        }

        Command ICarValueAppRepo.RunCommand(Root root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }
            Command r = new Command();

            int year = Convert.ToInt32(DateTime.Now.Year);
            if (root.year > year)
            {
                throw new ArgumentException("Invalid year Input!");
            }
            if ((root.yearOfPurchase > root.year))
            {
                throw new Exception("Invalid Year of purchase");
            }
            if (_context.carValueAPI.Any(r => r.PlateNumber == root.plateNumber))
            {
                throw new Exception("PlateNumber Already exists");
            }
            Command s = new Command
            {
                Make = root.make,
                Model = root.model,
                Year = root.year,
                Color = root.color,
                Price = root.price,
                YearOfPurchase = root.yearOfPurchase,
                Mileage = root.milage,
                PlateNumber = root.plateNumber,
                CarCurrentPrice = root.CurrentCarValue()
            };
            _context.carValueAPI.Add(s);
            _context.SaveChanges();
            return s;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void CreateCommand(Command commandModel)
        {
            throw new NotImplementedException();
        }

        public string BuyCar(BuyCar buyCar)
        {
            Command carId = GetCommandByID(buyCar.car_id);
            string carDescription = $"{carId.Make} {carId.Model}";
            decimal carPrice = carId.CarCurrentPrice * 100;
            var client = new RestClient("https://api.paystack.co/charge");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer sk_test_7a9c00f00af0fe7f1a1d94a25b52ab058fc0f96f");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "__cfduid=d76be2410a6a3eecc7b3cf25f37d2109f1616065541; sails.sid=s%3A3TDdlGwczUeNRq7Pl3gx32WNnXlrJCUy.ZU78UsbjyCzgHj9j43gvJxaGgy06UCEyAN4U%2Bztu7fc");
            string k = string.Concat("{\"email\":\"", buyCar.email_address, "\", \"first_name\":\"", buyCar.firstname_input, "\",\"last_name\":\"", buyCar.surname_input, "\",\"phone\":\"", buyCar.phone_number, "\",\"amount\":\"", carPrice, "\", \"metadata\":{\"custom_fields\":[{\"value\":\"", carDescription, "\", \"display_name\": \"Payment for\", \"variable_name\": \"donation_for\"}]},    \"card\":{\"cvv\":\"081\", \"number\":\"507850785078507812\", \"expiry_month\":\"01\", \"expiry_year\":\"26\"},    \"pin\":\"1111\"}");
            request.AddParameter("application/json", k, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            buyCar.object_reference = response.Content;
            buyCar.SavingCustomerInfoToDB();
            return buyCar.object_reference;
        }

        public signUpModel SignUp(signUpDTO signUp)
        {
            int length = 7;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            var ordinary = res.ToString();
            var encoded = EncodePasswordToBase64(res.ToString());

            var sign = new signUpModel
            {
                FirstName = signUp.FirstName,
                Surname = signUp.Surname,
                PhoneNumber = signUp.PhoneNumber,
                EmailAddress = signUp.EmailAddress,
                Password = encoded
            }
            ;
            if (_context.signUpTable.Any(r => r.EmailAddress == sign.EmailAddress))
            {
                throw new Exception("Email already exists");
            }
            var email = ConfigurationManager.AppSettings["email"];
            var password = ConfigurationManager.AppSettings["password"];
            var body = $" Hello {signUp.Surname} {signUp.FirstName} thank you for signing up. Your password is {ordinary} , you can use your email and password to login now.";
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(email);
            message.To.Add(new MailAddress(signUp.EmailAddress));
            message.Subject = "SIGN UP MAIL";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = body;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for the gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(email, password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            _context.signUpTable.Add(sign);
            _context.SaveChanges();
            return sign;
        }

        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public signUpModel LogIn(LoginDTO login)
        {
            signUpModel bd = new signUpModel();
            bd.EmailAddress = login.LoginEmail;
            if (!(_context.signUpTable.Any(r => r.EmailAddress == bd.EmailAddress)))
            {
                throw new Exception("Email does not exist");
            }
            var apap = _context.signUpTable.FirstOrDefault(t => t.EmailAddress == bd.EmailAddress);
            string sds = apap.Password;
            string sss = DecodeFrom64(apap.Password);
            bool ftp = login.LoginPassword == sss;
            if (!(ftp))
            {
                throw new Exception("Password does not exist!");
            }
            return apap;
        }
        
    }   

}
