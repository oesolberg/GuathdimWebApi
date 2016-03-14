using System;

namespace GuahtdimWebApi.WebGui
{
	public class GuahtdimFullInfoModel
	{
		public DateTime LastUpdatedDateTime { get; set; }
		public double Temperature { get; set; }
		public double Humidity { get; set; }
		public DateTime LastPumpOn { get; set; }
		public TimeSpan PumpRunTime { get; set; }
		public bool ReservoirEmpty { get; set; }
		public bool GrowPoolEmpty { get; set; }
		public bool GrowPoolFull { get; set; }
		public bool HeaterOn { get; set; }
		public bool IsPumping { get; set; }

		public bool HeaterDataExists { get; set; }
		public bool WaterlevelDataExists { get; set; }
		public bool PumpDataExists { get; set; }
		public bool TempAndHumidityDataExists { get; set; }

		public string TemperatureAsString => Temperature.ToString("0.00");

		public string HumidityAsString => Humidity.ToString("0.00");

		public string LastUpdatedDateTimeString => LastUpdatedDateTime.ToString(IsoDateTimeString);
		public string LastPumpOnDateTimeString => LastPumpOn.ToString(IsoDateTimeString);

		public string PumpRunTimeString => PumpRunTime.ToString(@"mm\:ss");

		private const string IsoDateTimeString = "yyyy-MM-dd HH:mm:ss";

		public void SetLastUpdatedDateTime(DateTime dateTime)
		{
			if (dateTime > LastUpdatedDateTime)
			{
				LastUpdatedDateTime = dateTime;
			}
		}
	}
}