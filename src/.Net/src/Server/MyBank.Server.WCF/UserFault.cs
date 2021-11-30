using System;
using System.Runtime.Serialization;

namespace MyBank.Server.WCF
{
    [DataContract]
    [Serializable()]
    public class UserFault
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Type { get; set; }
        public UserFault(Exception exception)
        {
            Message = exception.Message;
            Type = exception.GetType().Name;
        }
    }
}
