using System;
using System.Drawing;
using System.Windows.Forms;
using MyLib;
using Who_Will_Win_The_Million_Game.Screens;

namespace Who_Will_Win_The_Million_Game
{
    public partial class frmIntroLifeCycle : Form
    {
        Form frmOriginalLifeCycle;
        public frmIntroLifeCycle(Form frmOriginal) 
            {
            InitializeComponent();
            frmOriginalLifeCycle = frmOriginal;

            clsLib.ChangeFormProperties(this,Convert.ToInt16( this.Width), Convert.ToInt16(this.Height));

        }

        private void frmIntroLifeCycle_Load(object sender, EventArgs e)
        {
            guna2CircleProgressBar1.Hide();
        }
        Form frmMainScreen;
        private void btnStart_Click(object sender, EventArgs e)
        {
            clsLib.RunClickSound();

            btnStart.Hide();
            guna2CircleProgressBar1.Show();
            frmMainScreen = new frmMainScreen(this);
            timer1.Start();
            frmMainScreen.ShowDialog();
            frmMainScreen.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            guna2CircleProgressBar1.Value+=1;
            if (guna2CircleProgressBar1.Value == 100)
            {
                frmMainScreen.Enabled = true;
                
                timer1.Stop();
                frmMainScreen.Opacity = 100.0;
                this.Hide();
            }
        }

        private void btnStart_MouseHover(object sender, EventArgs e)
        {
            clsLib.RunHoverSound();
        }

        private void frmIntroLifeCycle_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(frmOriginalLifeCycle!=null)
            frmOriginalLifeCycle.Close();
        }
    }
}
