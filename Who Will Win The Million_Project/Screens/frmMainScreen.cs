using System;
using System.Windows.Forms;
using MyLib;
using Game;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using Who_Will_Win_The_Million_Game.Screens;

namespace Who_Will_Win_The_Million_Game
{
    public partial class frmMainScreen : Form
    {
        public frmMainScreen(Form frmLifeCycle)
        {
            InitializeComponent();
            frm1 = frmLifeCycle;
            clsLib.ChangeFormProperties(this, 1496, 1022);
        }

        private Form frm1;
        private short _Level;
        clsGame Game=new clsGame();
        bool CloseProgram = true;
        private void frmMainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(CloseProgram)
            frm1.Close();
        }
        private void _ShowAnswer(Button btn)
        {
            Game.ShowAnswer(btn,_Level);
        }
        private void _ShowQuestion()
        {
            Game.ShowQuestion( lblQuestion, _Level);
        }
        private void StartGame()
        {
            _ShowQuestion();
            _ShowAnswer(btnAnswer1);
            _ShowAnswer(btnAnswer2);
            _ShowAnswer(btnAnswer3);
            _ShowAnswer(btnAnswer4);
        }

        private void ReSetPropertiesToConitinue()
        {
            Game.FoundCorrectAnswer = false;
            Game.CounterForShowingAnswers = 0;
            btnRemove2Questions.Enabled = true;
            CountRemovedOptions = 0;
            arrRemovedButtons = new short[2];
            btnSwitchQuestion.Enabled = true;
            hoverOrClickSound = true;
            btnRemove2Questions.Enabled = true;
        }
        private void ContinueGame()
        {
            ReSetPropertiesToConitinue();
            StartGame();
            ReshowRemoveOptions();
        }
        private void frmMainScreen_Load(object sender, EventArgs e)
        {
            StartGame();
        }
        void ChangeBtnColor(Button btn,Color C)
        {
            btn.BackColor = C;
        }
        void ChangeButtonsColor(Button Choosedbtn)
        {
            if (Choosedbtn.Text == Game.Que.CurrentQRightAnswer)
            {
                ChangeBtnColor(Choosedbtn, Color.DarkGreen);

                if (Choosedbtn.Tag != btnAnswer1.Tag)
                    ChangeBtnColor(btnAnswer1, Color.DarkRed);
                if (Choosedbtn.Tag != btnAnswer2.Tag)
                    ChangeBtnColor(btnAnswer2, Color.DarkRed);
                if (Choosedbtn.Tag != btnAnswer3.Tag)
                    ChangeBtnColor(btnAnswer3, Color.DarkRed);
                if (Choosedbtn.Tag != btnAnswer4.Tag)
                    ChangeBtnColor(btnAnswer4, Color.DarkRed);
            }
            else
            {
                ChangeBtnColor(Choosedbtn, Color.DarkRed);
            }
        }
        int AddBalanceBasedOnLevel()
        {
            switch (_Level+1)
            {
                case 1:
                    return 100;
                case 2:
                    return 200;
                case 3:
                    return 400;
                case 4:
                    return 800;
                case 5:
                    return 1000;
                case 6:
                    return 2000;
                case 7:
                    return 4000;
                case 8:
                    return 8000;
                case 9:
                    return 16000;
                case 10:
                    return 32000;
                case 11:
                    return 65000;
                case 12:
                    return 125000;
                case 13:
                    return 250000;
                case 14:
                    return 500000;
                case 15:
                    return 1000000;
                default:
                    return 0;
            }
        }
        void ChangeLabelColor(Label lbl, Color BackColor,Color ForeColor)
        {
            lbl.BackColor = BackColor;
            lbl.ForeColor = ForeColor;
        }
        void ChangeLevelUp()
        {
            switch (_Level)
            {
                case 1:
                    ChangeLabelColor(lbl100, Color.Purple,Color.FromArgb(255,250,250,250));
                    break;
                case 2:
                    ChangeLabelColor(lbl200, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 3:
                    ChangeLabelColor(lbl400, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 4:
                    ChangeLabelColor(lbl800, Color. Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 5:
                    ChangeLabelColor(lbl1000, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 6:
                    ChangeLabelColor(lbl2Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 7:
                    ChangeLabelColor(lbl4Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 8:
                    ChangeLabelColor(lbl8Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 9:
                    ChangeLabelColor(lbl16Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 10:
                    ChangeLabelColor(lbl32Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 11:
                    ChangeLabelColor(lbl65Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 12:
                    ChangeLabelColor(lbl125Th, Color.Purple , Color.FromArgb(255, 250, 250, 250));
                    break;
                case 13:
                    ChangeLabelColor(lbl250Th, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 14:
                    ChangeLabelColor(lblHalfMillion, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
                case 15:
                    ChangeLabelColor(lblMillion, Color.Purple, Color.FromArgb(255, 250, 250, 250));
                    break;
            }
        }

        bool hoverOrClickSound = true;
        bool ValidateAnswer(Button btn)
        {
            btnSwitchQuestion.Enabled = false;
            btnRemove2Questions.Enabled = false;
            hoverOrClickSound = false ;
            if (btn.Text == Game.Que.CurrentQRightAnswer)
            {
                lblCurrentBalanceMoney.Text = AddBalanceBasedOnLevel().ToString() + " $";
                _Level++;
                ChangeLevelUp();
                return true;
            }
            else
                return false;
         
        }
        void EndGame(string Title,string Balance)
        {
            Form frmBalance = new frmBalanceCard(Title,Balance);
            frmBalance.ShowDialog();
            Form FrmStart = new frmIntroLifeCycle(frm1);
            //This Won't work because sometime the object reference still not disposed at the moment we are checking so the condition will be false
            //if (frmBalance.IsDisposed)
            //    {
            //    CloseProgram = false;
            //    this.Close();
            //    frm1.Show();
            //}
            CloseProgram = false;
            this.Close();
            FrmStart.Show();
        }
        bool Lose = false;
        private void CheckAnswer(Button btn)
        {
            if (Lose = (!ValidateAnswer(btn)))
            {
                if (!_Mute)
                    clsLib.RunLoseOrLeaveSound();
                if(lblCurrentBalanceMoney.Text!="0 $")
                EndGame("You Lost 1 Million Prize , Congrats About What You Earned And Better Luck Next Time To Get The 1 Million Prize", lblCurrentBalanceMoney.Text);
                else
                    EndGame("You Lost The Game, Better Luck Next Time", lblCurrentBalanceMoney.Text);
            }
            else if (_Level == 15)
            {
                if(!_Mute)
                clsLib.RunWiningSound();
                EndGame("Congratulations , you Have Won The 1 Million Dollar Prize", lblCurrentBalanceMoney.Text);
            }

            if (!Lose)
            {
                ChangeButtonsColor(btn);
                timer1.Start();
            }
           
        }

        bool AnswerValidated = false;
        private void Button_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Text==Game.Que.CurrentQRightAnswer && !_Mute)
                clsLib.RunWiningSound();

            else if (hoverOrClickSound && !_Mute)
                clsLib.RunClickSound();

            if (!AnswerValidated)
            CheckAnswer((Button)sender);

            AnswerValidated = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            AnswerValidated = false;
            ChangeBtnColor(btnAnswer1, Color.Purple);
            ChangeBtnColor(btnAnswer2, Color.Purple);
            ChangeBtnColor(btnAnswer3, Color.Purple);
            ChangeBtnColor(btnAnswer4, Color.Purple);
            if(_Level!=15)
            ContinueGame();
            
            
        }
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseProgram = false;
            Form frmMain = new frmMainScreen(frm1);
            frmMain.Opacity = 100.0;
            frmMain.Show();
            this.Close();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)==DialogResult.OK)
            this.Close();
        }

        short [] arrRemovedButtons =new short[2];
        bool CheckBtnIfIsRemoved(short BtnNumber)
        {
            if (arrRemovedButtons[0]== BtnNumber)
                return true;

            if (arrRemovedButtons[1] == BtnNumber)
                return true;

            return false;

        }
        short CountRemovedOptions;
        void remove1Option()
        {
            short RandomNum = clsLib.GetARandomNumber(1, 5);

            switch (RandomNum)
            {
                case 1:
                    if ((btnAnswer1.Text != Game.Que.CurrentQRightAnswer)&& !CheckBtnIfIsRemoved(1))
                    {
                        arrRemovedButtons[CountRemovedOptions] = 1;
                        btnAnswer1.Hide();
                        CountRemovedOptions++;
                    }
                    else
                    {
                        remove1Option();
                    }
                    break;
                case 2:
                    if ((btnAnswer2.Text != Game.Que.CurrentQRightAnswer) && !CheckBtnIfIsRemoved(2))
                    {
                        arrRemovedButtons[CountRemovedOptions] = 2;
                        btnAnswer2.Hide();
                        CountRemovedOptions++;
                    }
                    else
                    {
                        remove1Option();
                        
                    }
                    break;
                case 3:
                    if ((btnAnswer3.Text != Game.Que.CurrentQRightAnswer) && !CheckBtnIfIsRemoved(3))
                    {
                        arrRemovedButtons[CountRemovedOptions] = 3;
                        btnAnswer3.Hide();
                        CountRemovedOptions++;
                    }
                    else
                    {
                        remove1Option();
                        
                    }
                    break;
                case 4:
                    if ((btnAnswer4.Text != Game.Que.CurrentQRightAnswer) && !CheckBtnIfIsRemoved(4))
                    {
                        arrRemovedButtons[CountRemovedOptions] = 4;
                        btnAnswer4.Hide();
                        CountRemovedOptions++;
                    }
                    else
                    {
                        remove1Option();
                        
                    }
                    break;
            }
            
        }
        void ReshowOption(Button Btn)
        {
            if (!Btn.Visible)
                Btn.Show();
        }
        void ReshowRemoveOptions()
        {
            ReshowOption(btnAnswer1);
            ReshowOption(btnAnswer2);
            ReshowOption(btnAnswer3);
            ReshowOption(btnAnswer4);
            Removed = false;
        }
        bool Removed = false;
        private void btnRemove2Questions_Click(object sender, EventArgs e)
        {
            if (!Removed)
            {
                btnRemove2Questions.Enabled = false;  

                if(!_Mute)
                clsLib.RunClickSound();

                remove1Option();
                remove1Option();
            }

            Removed = true;
        }
        private void btnSwitchQuestion_Click(object sender, EventArgs e)
        {
            if(!_Mute)
            clsLib.RunClickSound();

            ContinueGame();
        }
        private void btnLeave_Click(object sender, EventArgs e)
        {
            if(!_Mute)
            clsLib.RunLoseOrLeaveSound();

            if (MessageBox.Show("Are you sure you want to exit?\n", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                EndGame("You Left The Game , Congrats About What You Earned And Better Luck Next Time To Get The 1 Million Prize", lblCurrentBalanceMoney.Text);
        }
        private void Controls_MouseHover(object sender, EventArgs e)
        {
            if(hoverOrClickSound && !_Mute)
            clsLib.RunHoverSound();
        }

        private void ColorForeChange_TSMI_DropDownOpening(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).ForeColor = Color.Black;
        }
        private void ColorForeChange_TSMI_DropDownClosed(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).ForeColor = Color.FromArgb(255,226,226,226);
        }
        bool _Mute = false;
        private void muteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (muteToolStripMenuItem.Checked)
                _Mute = true;
            else
                _Mute = false;
        }
    }
}
