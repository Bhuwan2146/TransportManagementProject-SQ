﻿/*
 * Filename: Buyer.cs
 * Project: Transport Management System
 * Date: December 15, 2023
 * Author: Bhuwan Shrestha, Ahmed Alemleh, Smaran Adhikari
 * Description: It serves as a controller for the role C ContractMarketPlace.
 */
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TransportManagement
{
    public class ContractMarketplace
    {
        public string MarketPlaceServer {  get; set; }
        public string MarketPlaceDBName {  get; set; }  
        public string MarketPlaceUID { get; set; }   
        public string MarketPlacePassword {  get; set; }    
        public int MarketPlacePort {  get; set; }

        public ContractMarketplace() 
        {
            MarketPlaceServer = "159.89.117.198";
            MarketPlaceDBName = "cmp";
            MarketPlacePassword = "Snodgr4ss!";
            MarketPlaceUID = "DevOSHT";
            MarketPlacePort = 3306;
        }
        public string ConnectMarketPlace()
        {
            return $"SERVER={MarketPlaceServer};DATABASE={MarketPlaceDBName};PORT={MarketPlacePort};UID={MarketPlaceUID};PASSWORD={MarketPlacePassword}";
        }

        public List<Contract> GetContracts()
        {
            List<Contract> contracts = new List<Contract>();
            try
            {
               
                using (MySqlConnection con = new MySqlConnection(ConnectMarketPlace()))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract", con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Contract cons = new Contract
                            {
                                ClientName = rdr["CLIENT_NAME"].ToString(),
                                JobType = (JobType)int.Parse(rdr["JOB_TYPE"].ToString()),
                                Quantity = int.Parse(rdr["QUANTITY"].ToString()),
                                Origin = (City)Enum.Parse(typeof(City), rdr["ORIGIN"].ToString(), true),
                                Destination = (City)Enum.Parse(typeof(City), rdr["DESTINATION"].ToString(), true),
                                VanType = (VanType)int.Parse(rdr["VAN_TYPE"].ToString())
                            };
                            contracts.Add(cons);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log($"Cannot Connect to the MarketPlace Database: {e.Message}", LogLevel.Error);
            }

            return contracts;
        }



    }
}
