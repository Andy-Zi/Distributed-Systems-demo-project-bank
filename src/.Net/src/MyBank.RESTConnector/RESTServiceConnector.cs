using MyBank.Interfaces;
using MyBank.Nameservice.Exceptions;
using MyBank.Server.Backend;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MyBank.RESTConnector
{
    public class RESTServiceConnector : IServiceConnector
    {
        readonly Serializer serializer = new Serializer();
        RestClient client;


        protected TType MakeRequest<TType>(RestRequest restRequest)
        {
            var response = client.Get(restRequest);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.NoContent:
                    return default;
                case System.Net.HttpStatusCode.OK:
                    //Deserialize the content to the Traget Type using the same Serializer as the Backend
                    using (TextReader sr = new StringReader(response.Content))
                    {
                        using (JsonReader reader = new JsonTextReader(sr))
                        {
                            return serializer.Deserialize<TType>(reader);
                        }
                    }
                       
                case System.Net.HttpStatusCode.InternalServerError:
                    var json = (JObject)JToken.Parse(response.Content);
                    var message = json["exceptionMessage"].ToString();
                    var type = json["exceptionType"].ToString();
                    switch (type.Substring(type.LastIndexOf('.')+1))
                    {
                        case nameof(AuthenticationException):
                            throw new AuthenticationException(message);
                        case nameof(LoginException):
                            throw new LoginException(message);
                        case nameof(ArgumentException):
                            throw new ArgumentException(message);
                        default:
                            throw new Exception(message);
                    }
                default:
                    if (response.ErrorException is WebException webException)
                    {
                        if (webException.Status == WebExceptionStatus.ConnectFailure || webException.Status == WebExceptionStatus.Timeout)
                        {
                            throw new ServerNotReachableException(webException);
                        }
                    }
                    throw new Exception($"An error occured!\n{response.ErrorMessage}");
            }
        }
        public void Connect(string address, int port)
        {
            client = new RestClient($"http://{address}:{port}/bank/");
            client.Timeout = 5000;
        }

        public void Disconnect()
        {
            client = null;
        }

        public bool IsConnected()
        {
            return client != null;
        }

        public void Bye(string token)
        {
            var request = new RestRequest($"Bye?token={token}", DataFormat.Json);
            MakeRequest<string>(request);
        }


        public List<(string AccountNumber, string Description)> ListAccounts(string token)
        {
            var request = new RestRequest($"ListAccounts?token={token}", DataFormat.Json);
            var response = MakeRequest<List<(string AccountNumber, string Description)>>(request);
            return response;
        }

        public string Login(string username, string password)
        {
            var request = new RestRequest($"Login?username={username}&password={password}", DataFormat.Json);
            return MakeRequest<string>(request);
        }

        public string NewAccount(string token, string username, string description)
        {
            var request = new RestRequest($"NewAccount?token={token}&username={username}&description={description}", DataFormat.Json);
            return MakeRequest<string>(request);
        }

        public void NewUser(string token, string username, string password)
        {
            var request = new RestRequest($"NewUser?token={token}&username={username}&password={password}", DataFormat.Json);
            MakeRequest<string>(request);
        }

        public void PayInto(string token, string accountNumber, float amount)
        {
            var request = new RestRequest($"PayInto?token={token}&accountNumber={accountNumber}&amount={amount}", DataFormat.Json);
            MakeRequest<string>(request);
        }

        public List<IAccount> Statement(string token, string account_number = "", bool detailed = true)
        {
            var request = new RestRequest($"Statement?token={token}&account_number={account_number}&detailed={detailed}", DataFormat.Json);
            return MakeRequest<List<IAccount>>(request);
        }

        public void Transfere(string token, string from_accountNumber, string to_accountNumber, float amount, string comment = "")
        {
            var request = new RestRequest($"Transfere?token={token}&from_accountNumber={from_accountNumber}&to_accountNumber={to_accountNumber}&amount={amount}&comment={comment}", DataFormat.Json);
            MakeRequest<string>(request);
        }
    }
}
