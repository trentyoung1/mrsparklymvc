using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MrSparklyMVC.Models;
using System.Data.Entity;

namespace MrSparklyMVC.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RetailerService : IRetailerService
    {
        //Gets the total amount of the sales made to a certain retailer.
        public string GetTotalRetailerSales(int retailerID)
        {
            int retailerTotal = 0;
            MrSparklyEntities db = new MrSparklyEntities();

            //get all the orderlines belonging to a certain retailer.
            var salesorderLines = db.SalesOrderLines.SqlQuery("SELECT * FROM (Retailers INNER JOIN SalesOrders ON Retailers.retailerID = SalesOrders.retailerID) INNER JOIN SalesOrderLines ON SalesOrders.salesOrderID = SalesOrderLines.salesOrderID WHERE (((SalesOrders.retailerID)={0}))", retailerID);
            
            //add up the subtotals to get the total amount.
            foreach (var orderline in salesorderLines)
            {
                retailerTotal += (int)orderline.salesOrderLinesSubtotal;
            }

            return retailerTotal.ToString();
        }
    }
}
