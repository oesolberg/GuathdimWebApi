using System;

namespace GuahtdimWebApi.WebGui
{
	internal class PumpTimeData
	{
		public bool IsPumping { get; set; }
		public DateTime LastPumpDateTime { get; set; }
		public TimeSpan LastPumpTimeSpan { get; set; }

	}
}