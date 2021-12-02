using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace MyBank.Client
{
    public partial class BaseActionForm : Form
    {
        [Dependency] public ApplicationEnvironment ApplicationEnvironment;
        public BaseActionForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        protected override void OnShown(EventArgs e)
        {
            this.CenterToScreen();
            base.OnShown(e);
        }
        protected void HandleClose()
        {
            if(!ApplicationEnvironment.ServiceConnector.Faulted)
                this.Close();

        }
    }
}
