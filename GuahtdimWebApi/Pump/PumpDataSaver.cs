using System;
using DapperExtensions;

namespace GuahtdimWebApi.Pump
{
	public class PumpDataSaver
	{

		public void InsertPumpData(PumpData pumpData)
		{
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				//Person person = new Person { FirstName = "Foo", LastName = "Bar", Active = true, DateCreated = DateTime.Now };
				//int id = cn.Insert(person);
				int id = cn.Insert(pumpData);
				cn.Close();
			}

		}

	}
}