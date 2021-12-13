
using System.Collections.Generic;


namespace MyBank.CCOMConnector.Model
{
    internal class CCOMStatement
    {
        public CCOMAccount Account { get; set; }
        public List<CCOMTransaction> Transactions { get; set; }
    }
}
