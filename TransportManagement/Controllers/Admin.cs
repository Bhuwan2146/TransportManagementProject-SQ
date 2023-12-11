﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement
{
    public class Admin
    {
        DAL dal = new DAL();
       
        
        /*
         * Passes the fields to be changed to the DAL to change the Database
         */
        public void UpdateConnectionString(string fieldToChange, string newData)
        {
            dal.UpdateDatabaseConnectionString(fieldToChange, newData);
        }

        /*
         * Update LogPath 
         */

        public int UpdateLogPath(string newPath)
        {
           int result = Logger.ChangeLogDirectory(newPath);

            if (result == 0)
            {
                return 0;  //Successfully changed 
            }

            else
            {
                return 1; //False
            }
        }


        //*************************** Working related to the rate field **************************************

        public double GetFTLRate()
        {
            return dal.GetCurrentFTLRate();
        }

        public double GetLTLRate()
        {
            return dal.GetCurrentLTLRate();
        }
        public bool UpdateFTLRate(double newFTLRate)
        {
            return dal.UpdateFTLRate(newFTLRate);
        }

        public bool UpdateLTLRate(double newFTLRate)
        {
            return dal.UpdateLTLRate(newFTLRate);
        }


        //************************** related to the carrier ******************************
        public List<CarrierCity> GetCitiesByCarrier(string carrierName)
        {
            List<CarrierCity> carrierCities = dal.FilterCitiesByCarrier(carrierName);
            Logger.Log("Fetched CarrierCities", LogLevel.Information);
            return carrierCities;
        }
        public List<Carrier> FetchCarriers()
        {
            List<Carrier> carriers = dal.GetAllCarriers();
            return carriers;
        }

        public bool DeleteCarrier(Carrier carrier)
        {
            bool deleted = false; 
            deleted = dal.DeleteCarrierFromSystem(carrier);
            return deleted;
        }

        public int GetCarrierIDByName(string carrierName)
        {
            int carrierID = dal.GetCarrierIDByName(carrierName);
            return carrierID; 
        }

        public bool DeleteCarrierCity(CarrierCity carrierCity)
        {
            bool deleted = false;
            deleted = dal.DeleteCarrierCity(carrierCity);
            return deleted;
        }
    }
}
