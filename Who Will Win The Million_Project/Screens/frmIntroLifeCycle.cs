using Guna.UI2.WinForms;
using MyLib;
using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
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


            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) // Get Current Windows Display DPI , we used using for Graphics because it is non managed resource and we must dispose it to deallocate it from memory and by that using automatically dipose it .
            {
                float dpiX = g.DpiX;
                // dpix / 96f to have the display scale and * 100 to have it in percentage like 1.75 to be 175 .
                if (((dpiX / 96f) * 100) > 100)
                {
                    this.MaximizeBox = true;
                    this.WindowState = FormWindowState.Maximized;
                    guna2CircleProgressBar1.Location = new Point(230, 33);

                }

            }
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
            guna2CircleProgressBar1.BringToFront();
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
