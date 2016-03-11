using System.Diagnostics;
using GuahtdimWebApi.Pump;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace GuahtdimWebApi.Pump
{
	public class PumpModule:NancyModule
	{

		public PumpModule():base("/pump")
		{
			this.RequiresAuthentication();

			
			Post["/", true] = async (x, ctx) =>
			{
				PumpData pumpData = this.Bind<PumpData>();

				Debug.Write(pumpData.PumpOn);
				//Do saving with dapper
				var pumpSaver = new PumpDataSaver();
				pumpSaver.InsertPumpData(pumpData);

				return "pump data entered";
			};

		}
		 
	}
}