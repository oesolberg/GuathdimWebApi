using System;

namespace GuahtdimWebApi.TempAndHumidity
{

    public class TempAndCreatedDateTime
    {
        public float Temperature { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public string ToDataString()
        {
            
            return "[Date.UTC("+CreatedDateTime.Year.ToString()+"," + CreatedDateTime.Month.ToString() + "," +
                   CreatedDateTime.Day.ToString() + "," + CreatedDateTime.Hour.ToString() + "," + CreatedDateTime.Minute.ToString() + "," + 
                   CreatedDateTime.Second.ToString() + "," +
                   CreatedDateTime.Millisecond.ToString() + "),"
                   + Temperature + "]";
        }
    }
}
