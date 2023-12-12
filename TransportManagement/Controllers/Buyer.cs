﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement
{ 
    public class Buyer
    {
        DAL dal = new DAL();
        public List<Contract> GetContractsFromMarketPlaceDatabase()
        {
            List<Contract> list = new List<Contract>();
            ContractMarketplace marketplace = new ContractMarketplace();
            list = marketplace.GetContracts();
            return list;
        }

        public Order GenerateOrder(Contract contract)
        {
            Order order = new Order
            {
                ClientName = contract.ClientName,
                Origin = contract.Origin,
                Destination = contract.Destination,
                JobType = contract.JobType,
                Quantity = contract.Quantity,
                VanType = contract.VanType,
                OrderCreationDate = DateTime.Now,
                OrderAcceptedDate = DateTime.Now,
                InvoiceGenerated = 0, // Assuming 0 means false
                IsCompleted = 0,   // Assuming 0 means false
            };

            if(dal.GetClientByName(order.ClientName) == null)
            {
                Client client = new Client(order.ClientName);
                dal.CreateClient(client);
            }

            try
            {
                dal.SaveOrderToDatabase(order);
            }

            catch(Exception ex)
            {
                Logger.Log("Orer cannot be saved to the database!!", LogLevel.Error);
            }

            return order;


        }
    }
}
