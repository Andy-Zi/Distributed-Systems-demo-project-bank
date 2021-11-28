using MyBank.Interfaces;
using MyBank.Server.Backend.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace MyBank.Server.Backend
{
    public class Serializer : JsonSerializer
    {

        class Converter<TInterface,TClass> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(TInterface));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return serializer.Deserialize(reader, typeof(TClass));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value, typeof(TClass));
            }
        }

        public Serializer()
        {
            this.NullValueHandling = NullValueHandling.Ignore;
            this.Converters.Add(new Converter<ITransaction, Model.Transaction>());
            this.Converters.Add(new Converter<IAccount, Account>());
            this.Converters.Add(new Converter<IUser, User>());
        }
    }
}
