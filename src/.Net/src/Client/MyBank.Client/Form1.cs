using MyBank.WCFConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBank.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            WCFServiceConnector test = new WCFServiceConnector();
            test.Connect("http://localhost:8000/WCFBankService",-1);
            var token = test.Login("asd", "1234");
        }
    }
}
