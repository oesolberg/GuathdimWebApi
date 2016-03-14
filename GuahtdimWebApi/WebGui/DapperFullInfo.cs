using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Dapper;
using DapperExtensions;
using GuahtdimWebApi.Heater;
using GuahtdimWebApi.Pump;
using GuahtdimWebApi.TempAndHumidity;
using GuahtdimWebApi.Waterlevels;

namespace GuahtdimWebApi.WebGui
{
	internal class DapperFullInfo
	{
		public DapperFullInfo()
		{
		}

		public void InsertWaterlevelData(WaterLevelData waterLevelData)
		{
			var connectionString =GetConnectionString();
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				//Person person = new Person { FirstName = "Foo", LastName = "Bar", Active = true, DateCreated = DateTime.Now };
				//int id = cn.Insert(person);
				int id = cn.Insert(waterLevelData);
				cn.Close();
			}

		}

		public GuahtdimFullInfoModel GetFullInfo()
		{
			var waterlevelData = GetWaterLevelData();
			var pumpData = GetPumpData();
			var heaterData = GetHeaterData();
			var tempAndHumidity = GetTempAndHumidity();

			var fullViewModel = CreateViewModelWithGivenData(waterlevelData, pumpData, heaterData, tempAndHumidity);

			return fullViewModel;

		}

		private GuahtdimFullInfoModel CreateViewModelWithGivenData(WaterLevelData waterlevelData, PumpTimeData pumpData, HeaterData heaterData, TempAndHumidityData tempAndHumidityData)
		{
			var viewModel = new GuahtdimFullInfoModel();
			viewModel.SetLastUpdatedDateTime((DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
			if (waterlevelData != null)
			{
				viewModel.WaterlevelDataExists= true;
				viewModel.ReservoirEmpty = waterlevelData.ReservoirEmpty;
				viewModel.GrowPoolEmpty = waterlevelData.GrowPodEmpty;
				viewModel.GrowPoolFull = waterlevelData.GrowPodFull;
				viewModel.SetLastUpdatedDateTime(waterlevelData.CreatedDateTime); 
			}

			if (pumpData != null)
			{
				viewModel.PumpDataExists = true;
				viewModel.LastPumpOn = pumpData.LastPumpDateTime;
				viewModel.PumpRunTime = pumpData.LastPumpTimeSpan;
				viewModel.IsPumping = pumpData.IsPumping;
				viewModel.SetLastUpdatedDateTime(pumpData.LastPumpDateTime);
			}

			if (heaterData != null)
			{
				viewModel.HeaterDataExists= true;
				viewModel.HeaterOn = heaterData.HeatOn;
			
				viewModel.SetLastUpdatedDateTime(heaterData.CreatedDateTime);
			}
			if (tempAndHumidityData != null)
			{
				viewModel.TempAndHumidityDataExists= true;
				viewModel.Temperature = tempAndHumidityData.Temperature;
				viewModel.Humidity = tempAndHumidityData.Humidity;
				viewModel.SetLastUpdatedDateTime(tempAndHumidityData.CreatedDateTime);
			}
			return viewModel;
		}

		private PumpTimeData GetPumpData()
		{
			var connectionString = GetConnectionString();

			//1. Last status
			var lastStatusData = GetLastPumpStatus(connectionString);
			if (lastStatusData == null) return null;
			//2. Last on (if not last status is on)
			var pumpTimeData = new PumpTimeData()
			{
				IsPumping = lastStatusData.PumpOn
			};

			PumpData lastPumpOnData = null;
			if (!lastStatusData.PumpOn)
			{
				lastPumpOnData = GetLastPumpOn(connectionString);
			}

			//3. first off after last on
			PumpData pumpOffAfterPumpOn = null;
			if (lastPumpOnData != null)
			{
				pumpOffAfterPumpOn = GetPumpOffAfterPumpOn(connectionString, lastPumpOnData.Id, lastStatusData.Id);
				if (pumpOffAfterPumpOn != null)
				{

					pumpTimeData.LastPumpDateTime = lastPumpOnData.CreatedDateTime;
					pumpTimeData.LastPumpTimeSpan = pumpOffAfterPumpOn.CreatedDateTime - lastPumpOnData.CreatedDateTime;
				}
			}
			return pumpTimeData;
		}

		private PumpData GetPumpOffAfterPumpOn(string connectionString, int pumpOnId, int lastInfoId)
		{

			var searchForId = pumpOnId + 1;
			PumpData foundPumpData = null;
			while (foundPumpData == null && searchForId <= lastInfoId)
			{
				foundPumpData = GetPumpDataById(connectionString,searchForId);
				if (foundPumpData != null) return foundPumpData;
				searchForId++;
			}

			return null;
		}

		private PumpData GetPumpDataById(string connectionString, int searchForId)
		{
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				var foundLastPumpOn = cn.Query<PumpData>("Select top 1 * from PumpData" +
																	" Where [PumpOn]=0 " +
																	" and Id=@id " +
																   " order by id desc", new { id = searchForId} );
				cn.Close();
				if (foundLastPumpOn.Count() == 1)
				{

					return foundLastPumpOn.FirstOrDefault();

				}
			}
			return null;
		}

		private PumpData GetLastPumpOn(string connectionString)
		{
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				var foundLastPumpOn = cn.Query<PumpData>("Select top 1 * from PumpData" +
																	" Where [PumpOn]=1" +
																   " order by id desc");
				cn.Close();
				if (foundLastPumpOn.Count() == 1)
				{	
					return foundLastPumpOn.FirstOrDefault();
				}
			}
			return null;
		}

		private PumpData GetLastPumpStatus(string connectionString)
		{
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				var foundLastPumpOn = cn.Query<PumpData>("Select top 1 * from PumpData" +
																   " order by id desc");
				cn.Close();
				if (foundLastPumpOn.Count() == 1)
				{
					
					return foundLastPumpOn.FirstOrDefault();
				}
			}
			return null;
		}

		private static string GetConnectionString()
		{
			return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		}

		private HeaterData GetHeaterData()
		{
			var connectionString = GetConnectionString();
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				//Person person = new Person { FirstName = "Foo", LastName = "Bar", Active = true, DateCreated = DateTime.Now };
				//int id = cn.Insert(person);
				var foundHeaterData = cn.Query<HeaterData>("Select top 1 * from HeaterData order by id desc");
				cn.Close();
				if (foundHeaterData.Count() == 1)
				{
					return foundHeaterData.FirstOrDefault();
				}
				return null;
			}
		}

		private TempAndHumidityData GetTempAndHumidity()
		{
			var connectionString = GetConnectionString();
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				
				var foundTempAndHumidity = cn.Query<TempAndHumidityData>("Select top 1 * from TempAndHumidityData order by id desc");
				cn.Close();
				if (foundTempAndHumidity.Count() == 1)
				{
					return foundTempAndHumidity.FirstOrDefault();
				}
				return null;
			}
		}

		private WaterLevelData GetWaterLevelData()
		{
			var connectionString = GetConnectionString();
			using (var cn = new System.Data.SqlClient.SqlConnection(connectionString))
			{
				cn.Open();
				//Person person = new Person { FirstName = "Foo", LastName = "Bar", Active = true, DateCreated = DateTime.Now };
				//int id = cn.Insert(person);
				var foundWaterLevelData = cn.Query<WaterLevelData>("Select top 1 * from WaterLevelData order by id desc");
				cn.Close();
				if (foundWaterLevelData.Count() == 1)
				{
					return foundWaterLevelData.FirstOrDefault();
				}
				return null;
			}

		}
	}
}