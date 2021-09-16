using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarValueApi.Model
{
    public class Root
    {
        /// <summary>
        /// Make input of the car
        /// </summary>
        public string make { get; set; }
        /// <summary>
        /// Model input of the car
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// Year input of the car
        /// </summary>
        public int year { get; set; }
        /// <summary>
        /// Year of purchase input of the car
        /// </summary>
        public int yearOfPurchase { get; set; }
        /// <summary>
        /// Price input of the car
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// Color input of the car
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// Milage input of the car
        /// </summary>
        public int milage { get; set; }
        /// <summary>
        /// Plate number input of the car
        /// </summary>
        public string plateNumber { get; set; }
        

        internal decimal DetermineMarketValue()
        {


            decimal CarYearValue;

            if (year < 2000)
            {
                //myCarValue = Convert.ToInt32(PriceBought * 0.42);
                CarYearValue = -Convert.ToInt32(price * 0.20);

            }
            else if (year >= 2000 || year <= 2001)
            {
                CarYearValue = -Convert.ToInt32(price * 0.18);
            }
            else if (year >= 2002 || year <= 2003)
            {
                CarYearValue = -Convert.ToInt32(price * 0.16);
            }
            else if (year >= 2004 || year <= 2005)
            {
                CarYearValue = -Convert.ToInt32(price * 0.14);
            }
            else if (year >= 2006 || year <= 2007)
            {
                CarYearValue = -Convert.ToInt32(price * 0.12);
            }
            else if (year >= 2008 || year <= 2009)
            {
                CarYearValue = -Convert.ToInt32(price * 0.10);
            }
            else if (year >= 2010 || year <= 2011)
            {
                CarYearValue = -Convert.ToInt32(price * 0.08);
            }
            else if (year >= 2012 || year <= 2012)
            {
                CarYearValue = price - Convert.ToInt32(price * 0.06);
            }
            else if (year >= 2013 || year <= 2014)
            {
                CarYearValue = -Convert.ToInt32(price * 0.04);
            }
            else if (year >= 2015 || year <= 2016)
            {
                CarYearValue = -Convert.ToInt32(price * 0.02);
            }
            else if (year >= 2017 || year <= 2018)
            {
                CarYearValue = -Convert.ToInt32(price * 0.02);
            }
            else if (year >= 2019 || year <= 2020)
            {
                CarYearValue = -Convert.ToInt32(price * 0.01);
            }
            else if (year == 2021)
            {
                CarYearValue = -Convert.ToInt32(price * 0.01);
            }


            else
            {
                CarYearValue = price;
            }

            return CarYearValue;
        }

        internal decimal YearsUsed()
        {

            int CarValueAfterNumberOfYears;
            int numberOfYearsUsed;
            numberOfYearsUsed = yearOfPurchase - year;

            if (numberOfYearsUsed == 0)
            {
                CarValueAfterNumberOfYears = 0;
            }
            else if (numberOfYearsUsed == 1 || numberOfYearsUsed == 2)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.01);
            }
            else if (numberOfYearsUsed == 3 || numberOfYearsUsed == 4)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.02);
            }
            else if (numberOfYearsUsed == 5 || numberOfYearsUsed == 6)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.04);
            }
            else if (numberOfYearsUsed == 7 || numberOfYearsUsed == 8)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.06);
            }
            else if (numberOfYearsUsed == 9 || numberOfYearsUsed == 10)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.08);
            }
            else if (numberOfYearsUsed == 11 || numberOfYearsUsed == 12)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.10);
            }
            else if (numberOfYearsUsed == 13 || numberOfYearsUsed == 14)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.12);
            }
            else if (numberOfYearsUsed == 15 || numberOfYearsUsed == 16)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.14);
            }
            else if (numberOfYearsUsed == 17 || numberOfYearsUsed == 18)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.14);
            }
            else if (numberOfYearsUsed == 19 || numberOfYearsUsed == 20)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.16);
            }
            else if (numberOfYearsUsed == 21)
            {
                CarValueAfterNumberOfYears = -Convert.ToInt32(price * 0.16);
            }
            else
            {
                CarValueAfterNumberOfYears = 0;
            }
            return CarValueAfterNumberOfYears;
        }

        internal int Milage()
        {
            int carMilageValue;
            if (milage <= 20000)
            {
                carMilageValue = 0;
            }
            else if (milage >= 20000 && milage < 40000)
            {
                carMilageValue = -Convert.ToInt32(price * 0.05);
            }
            else if (milage >= 40000 && milage < 80000)
            {
                carMilageValue = -Convert.ToInt32(price * 0.10);
            }
            else if (milage >= 80000 && milage < 120000)
            {
                carMilageValue = -Convert.ToInt32(price * 0.15);
            }
            else if (milage >= 120000)
            {
                carMilageValue = -Convert.ToInt32(price * 0.20);
            }
            else
            {
                carMilageValue = 0;
            }

            return carMilageValue;
        }

        internal decimal CurrentCarValue()
        {

            decimal purr = Convert.ToDecimal(YearsUsed() + DetermineMarketValue() + Milage());
            decimal currentPrices = price + purr;


            return currentPrices;
        }

        
    }
}
