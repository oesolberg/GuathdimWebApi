using System;
using DapperExtensions;

namespace GuahtdimWebApi.Heater
{
	public class HeaterDataSaver
	{

		public void InsertHeatData(HeaterData heaterData)
		{
			var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				//Person person = new Person { FirstName = "Foo", LastName = "Bar", Active = true, DateCreated = DateTime.Now };
				//int id = cn.Insert(person);
				int id = cn.Insert(heaterData);
				cn.Close();
			}

		}

	}
}