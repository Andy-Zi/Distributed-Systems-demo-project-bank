using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyBank.Server.Backend
{
    public class ApplicationEnvironment
    {
        public string ApplicationDirectory { get; }

        public string DataDirectory { get; }
        public ApplicationEnvironment(string applicationDirectory)
        {
            this.ApplicationDirectory = applicationDirectory;
            this.DataDirectory = Path.Combine(this.ApplicationDirectory,"Data");
        }
    }
}
