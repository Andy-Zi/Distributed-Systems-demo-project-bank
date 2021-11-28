using System;
using System.Collections.Generic;
using System.Text;

namespace MyBank.Server.Backend.Model
{
    public interface IEntity
    {
        string GetMappingKey();
    }
}
