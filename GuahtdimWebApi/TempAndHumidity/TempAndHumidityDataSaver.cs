using System;
using DapperExtensions;

namespace GuahtdimWebApi.TempAndHumidity
{
	public class TempAndHumidityDataSaver
	{

		public void InsertTempAndHumidityData(TempAndHumidityData tempAndHumidityData)
		{
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				
				int id = cn.Insert(tempAndHumidityData);
				cn.Close();
			}

		}

	}
}