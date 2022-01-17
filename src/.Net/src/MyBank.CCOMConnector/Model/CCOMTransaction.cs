using MyBank.Interfaces;
using Newtonsoft.Json;
using System;

namespace MyBank.CCOMConnector.Model
{
    internal class CCOMTransaction : ITransaction
    {
        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonIgnore]
        public DateTime Date
        {
            get => UnixTimeStampToDateTime(time);
            set
            {
                //Do nothing
            }
        }

        [JsonIgnore]
        public string SenderAccount
        {
            get => startaccID.ToString();
            set => startaccID = Int32.Parse(value);
        }

        [JsonIgnore]
        public string RecieverAccount
        {
            get => endaccID.ToString();
            set => endaccID = Int32.Parse(value);
        }

        public int startaccID { get; set; }

        public int endaccID { get; set; }

        public long time { get; set; }

        public string RecieverName { get; set; }

        protected static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}