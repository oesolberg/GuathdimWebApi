using System;
using System.Collections.Generic;
using DapperExtensions;
using System.Linq;
using Dapper;
using DapperExtensions = DapperExtensions.DapperExtensions;

namespace GuahtdimWebApi.TempAndHumidity
{
    public class TempAndHumidityDataGetter
    {
        public List<TempAndCreatedDateTime> GetTempAndHumidityData()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                cn.Open();
                

                var foundData = cn.Query<TempAndCreatedDateTime>("Select CreatedDateTime, Temperature from TempAndHumidityData order by CreatedDateTime asc");
                cn.Close();
                if(foundData!=null)
                    return foundData.ToList();
            }
            return null;

        }
    }
}