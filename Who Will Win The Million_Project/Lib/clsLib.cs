using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace MyLib
{
    public class clsLib
    {
        public static void ChangeFormProperties(Form frm, short MaxWidth,short MaxHeight)
        {
            frm.StartPosition= FormStartPosition.CenterScreen;
            frm.MinimumSize = new Size(MaxWidth, MaxHeight);
            frm.MaximumSize=new Size(MaxWidth, MaxHeight);
        }
       static Random random = new Random();
        public static short GetARandomNumber(short From, short To)
        {

            short Number = Convert.ToInt16(random.Next(From, To));
            return Number;
        }

        public static void RunHoverSound()
        {
            SoundPlayer P=new SoundPlayer(@"D:\My Projects\Who-Will-Win-The-Million-C-.NET-WinForms-Game\Who Will Win The Million_Project\bin\Release\Hover.wav");
            P.Play();
        }
        public static void RunWiningSound()
        {
            SoundPlayer P = new SoundPlayer(@"D:\My Projects\Who-Will-Win-The-Million-C-.NET-WinForms-Game\Who Will Win The Million_Project\bin\Debug\Win.wav");
            P.Play();
        }

        public static void RunLoseOrLeaveSound()
        {
            SoundPlayer P = new SoundPlayer(@"D:\My Projects\Who-Will-Win-The-Million-C-.NET-WinForms-Game\Who Will Win The Million_Project\bin\Debug\Lose,Leave.wav");
            P.Play();
        }
        public static void RunClickSound()
        {
            SoundPlayer P = new SoundPlayer(@"D:\My Projects\Who-Will-Win-The-Million-C-.NET-WinForms-Game\Who Will Win The Million_Project\bin\Debug\Click.wav");
            P.Play();
        }
    }
}
