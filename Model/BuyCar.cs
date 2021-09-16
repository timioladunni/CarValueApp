using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Model
{
    public class BuyCar
    {

        /// <summary>
        /// The selected ID of the car you want to purchase
        /// </summary>
        public int car_id { get; set; }
        /// <summary>
        /// The Surname of the buyer
        /// </summary>
        public string surname_input { get; set; }
        /// <summary>
        /// The Firstname of the buyer
        /// </summary>
        public string firstname_input { get; set; }
        /// <summary>
        /// The Phone number of the buyer
        /// </summary>
        public string phone_number { get; set; }
        /// <summary>
        /// The email address of the buyer
        /// </summary>
        public string email_address { get; set; }
        
        public string object_reference { get; set; }

        public void SavingCustomerInfoToDB()
        {

            SqlConnection con;
            SqlCommand cdd;
            var sqlKey = ConfigurationManager.AppSettings["SQLkey"];
            con = new SqlConnection($"{sqlKey}");
            cdd = new SqlCommand("insert into customer_info2(id,Surname,FirstName,PhoneNumber,EmailAddress,PaymentInfo) values ('" + car_id + "','" + surname_input + "','" + firstname_input + "','" + phone_number + "','" + email_address + "','" + object_reference + "')", con);

            con.Open();
            int v = cdd.ExecuteNonQuery();
            if (v != 0)
            {
                Console.WriteLine("Saved");
            }


        }
    }
}
