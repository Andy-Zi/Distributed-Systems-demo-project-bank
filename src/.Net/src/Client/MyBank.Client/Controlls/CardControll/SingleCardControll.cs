using System.Drawing;
using System.Windows.Forms;

namespace MyBank.Client.Controlls
{
    public partial class SingleCardControll : UserControl
    { 
        public Color HoverColor { get; set; } = Color.LightSkyBlue;
        public Color Color { get; set; } = Color.White;
        public SingleCardControll()
        {
            InitializeComponent();
        }

        public CardViewModel ViewModel { get; set; }

        public SingleCardControll(CardViewModel viewModel)
        {
            ViewModel = viewModel;
            this.BackColor = Color;
            InitializeComponent();
            label_accountNumber.MouseDoubleClick += Label_MouseDoubleClick;
            label_description.MouseDoubleClick += Label_MouseDoubleClick;
        }

        private void Label_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)

        {
            base.OnPaint(e);

            Color borderColor = SystemColors.AppWorkspace;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, borderColor, ButtonBorderStyle.Solid);
        }

        public void MarkSelected()
        {
            SuspendLayout();
            this.label_accountNumber.BackColor = HoverColor;
            this.label_description.BackColor = HoverColor;
            this.BackColor = HoverColor;
            ResumeLayout();
        }

        public void MarkUnselected()
        {
            SuspendLayout();
            this.label_accountNumber.BackColor = Color;
            this.label_description.BackColor = Color;
            this.BackColor = Color;
            ResumeLayout();
        }

        public void DataBind()
        {
            SuspendLayout();

            label_accountNumber.Text = ViewModel.AccountNumber;
            label_description.Text = ViewModel.Description;

            ResumeLayout();
        }
    }
}
