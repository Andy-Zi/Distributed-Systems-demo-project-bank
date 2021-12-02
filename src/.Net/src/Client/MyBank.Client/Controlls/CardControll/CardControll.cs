using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyBank.Client.Controlls
{
    public class CardsPanelClickedEventArgs : EventArgs
    {
        public CardViewModel ViewModel;
        public CardsPanelClickedEventArgs(CardViewModel item)
        {
            ViewModel = item;
        }
    }
    public class CardsPanel : FlowLayoutPanel
    {
        public int CardWidth = 228;
        public int CardHeight = 60;
        public event EventHandler<CardsPanelClickedEventArgs> CardPanelClicked;
        public event EventHandler<CardsPanelClickedEventArgs> CardPanelDoubleClicked;

        public ObservableCollection<CardViewModel> Cards = new ObservableCollection<CardViewModel>();

        public CardsPanel()
        {
            this.AutoScroll = true;
            this.Width = CardWidth+2;
        }

        public void AddCard(CardViewModel card)
        {
            Cards.Add(card);
            DataBind();
        }

        public void DataBind()
        {
            SuspendLayout();
            Controls.Clear();

            for (int i = 0; i < Cards.Count; i++)
            {
                var newCtl = new SingleCardControll(Cards[i]);
                newCtl.DataBind();
                SetCardControlLayout(newCtl, i);
                ((UserControl)newCtl).MouseDoubleClick += CardsPanel_MouseDoubleClick;
                ((UserControl)newCtl).MouseClick += CardsPanel_MouseClick;

                Controls.Add(newCtl);
            }
            ResumeLayout();
        }

        private void CardsPanel_MouseClick(object sender, MouseEventArgs e)
        {
            SelectPannel(((SingleCardControll)sender));
            CardPanelClicked?.Invoke(this, new CardsPanelClickedEventArgs(((SingleCardControll)sender).ViewModel));
        }

        SingleCardControll lastPannel;
        private void SelectPannel(SingleCardControll sender)
        {
            lastPannel?.MarkUnselected();
            sender.MarkSelected();
            lastPannel = sender;
        }

        private void CardsPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectPannel(((SingleCardControll)sender));
            CardPanelDoubleClicked?.Invoke(this, new CardsPanelClickedEventArgs(((SingleCardControll)sender).ViewModel));
        }

        void SetCardControlLayout(SingleCardControll ctl, int atIndex)
        {
            ctl.Width = CardWidth;
            ctl.Height = CardHeight;
        }
    }
}
