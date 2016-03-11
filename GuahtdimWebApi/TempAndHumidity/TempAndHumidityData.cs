using System;

namespace GuahtdimWebApi.TempAndHumidity
{
	public class TempAndHumidityData
	{
		public int Id { get; set; }
		public float Humidity { get; set; }
		public float Temperature { get; set; }
		public DateTime CreatedDateTime { get; set; }
	}
}