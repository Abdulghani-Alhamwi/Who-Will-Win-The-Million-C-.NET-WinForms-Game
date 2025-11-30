using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyLib;

namespace Who_Will_Win_The_Million_Game.Screens
{
    public partial class frmBalanceCard : Form
    {
        public frmBalanceCard(string Title,string Balance)
        {
            InitializeComponent();
            lblTitle.Text = Title;
            lblBalance.Text = Balance;
            clsLib.ChangeFormProperties(this, 683, 396);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            clsLib.RunClickSound();
            this.Close();
        }

        private void btnOK_MouseHover(object sender, EventArgs e)
        {
            clsLib.RunHoverSound();
        }
    }
}
