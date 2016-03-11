using System;

namespace GuahtdimWebApi.Waterlevels
{
	public class WaterLevelData
	{

		public int Id { get; set; }
		public bool ReservoirEmpty { get; set; }

		public bool GrowPodEmpty { get; set; }

		public bool GrowPodFull { get; set; }
		public DateTime CreatedDateTime { get; set; }
	}
}