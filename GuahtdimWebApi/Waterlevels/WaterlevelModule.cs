using System.Diagnostics;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace GuahtdimWebApi.Waterlevels
{
	public class WaterlevelModule:NancyModule
	{

		public WaterlevelModule():base("/waterlevels")
		{
			this.RequiresAuthentication();
			

			Post["/", true] = async (x, ctx) =>
			{
				WaterLevelData waterlevelData = this.Bind<WaterLevelData>();

				Debug.Write(waterlevelData.ReservoirEmpty);
				//Do saving with dapper
				var waterlevelSaver = new WaterlevelDataSaver();
				waterlevelSaver.InsertWaterlevelData(waterlevelData);

				DoWaterLevelLogic(waterlevelData);

				return "waterlevel data entered";
			};

		}

		private void DoWaterLevelLogic(WaterLevelData waterLevelData)
		{
			if (waterLevelData.ReservoirEmpty)
			{
				SendGridMail.SendGridMail.SendEmptyReservoirMail();
			}
		}
	}
}