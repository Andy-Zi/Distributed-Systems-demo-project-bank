using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyBank.Mobile.Models
{
    public class Statement_item
    {
        public string Accountnumber { get; set; }
        public string Description { get; set; }
        public Statement_item((string Accountnumber,string Description) statement)
        {
            this.Accountnumber = statement.Accountnumber;
            this.Description = statement.Description;
        }
    }
}