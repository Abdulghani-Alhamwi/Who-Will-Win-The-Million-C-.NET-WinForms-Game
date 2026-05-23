using Microsoft.SqlServer.Server;
using MyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace Game
{
    internal class clsGame
    {
        string _FileName = "FileQuestions.txt";
      internal List<(string Question, string RightAnswer, List<string> WrongAnswers)> lQuestions;
        public clsGame()
        {
            InitializeData();
        }
       public short CounterForShowingAnswers;
       public bool FoundCorrectAnswer =false;
        private List<string> _GetWrongAnswers ( string[] QuestionWithAnswers)
        {
            List<string> WrongAnswers = new List<string>();

            for (short i = 2 ; i < QuestionWithAnswers.Length; i++)
            {
                WrongAnswers.Add(QuestionWithAnswers[i]);
            }
            return WrongAnswers;
        }
        private List<(string Question, string RightAnswer, List<string> WrongAnswers)> _LoadDataFromFile(string FileName , string QuestionLevelSepartor , char QueSeparator, short RowsQNumber, short ColsLevelNumber)
        {
            List<(string Question, string RightAnswer, List<string> WrongAnswers)> lQuestions = new List<(string Question, string RightAnswer, List<string> WrongAnswers)> { };

            short CurrentLevel = 0;
            short CurrentRow = 0;

            if (!File.Exists(FileName))
                return null;

            using (StreamReader F = new StreamReader(FileName))
            {

                string Line;
                while ((Line = F.ReadLine()) != null)
                {
                    if (Line.StartsWith(QuestionLevelSepartor))
                    {
                        CurrentRow = 0;
                        continue;
                    }

                    string[] QuestionWithAnswers = Line.Split(QueSeparator);
                    lQuestions.Add((QuestionWithAnswers[0] , QuestionWithAnswers[1] , _GetWrongAnswers(QuestionWithAnswers)));
                  
                    CurrentRow++;

                    if (CurrentRow == 5)
                    {
                        CurrentLevel++;
                    }
                }
            }
            return lQuestions;
        }
        
        public void InitializeData()
        {
            //  _LoadDataFromFile(_FileName, Que.Questions, Que.RigthAnswers, Que.WrongAnswers, "#::#", ';', 5, 15);
            // 75 Questions .
            lQuestions = _LoadDataFromFile(_FileName, "#::#", ';', 5, 15);
        }

        private void _TakeRandomNumbersScaleBasedOnLevel(short Level,out short FirstRandomNum , out short SecondRandomNum)
        {
            switch (Level)
            {
                case 0:
                    FirstRandomNum = 0;
                    SecondRandomNum = 5;
                    break;

                case 1:
                    FirstRandomNum = 5;
                    SecondRandomNum = 10;
                    break;

                case 2:
                    FirstRandomNum = 10;
                    SecondRandomNum = 15;
                    break;

                case 3:
                    FirstRandomNum = 15;
                    SecondRandomNum = 20;
                    break;

                case 4:
                    FirstRandomNum = 20;
                    SecondRandomNum = 25;
                    break;

                case 5:
                    FirstRandomNum = 25;
                    SecondRandomNum = 30;
                    break;

                case 6:
                    FirstRandomNum = 30;
                    SecondRandomNum = 35;
                    break;

                case 7:
                    FirstRandomNum = 35;
                    SecondRandomNum = 40;
                    break;

                case 8:
                    FirstRandomNum = 40;
                    SecondRandomNum = 45;
                    break;

                case 9:
                    FirstRandomNum = 45;
                    SecondRandomNum = 50;
                    break;

                case 10:
                    FirstRandomNum = 50;
                    SecondRandomNum = 55;
                    break;

                case 11:
                    FirstRandomNum = 55;
                    SecondRandomNum = 60;
                    break;

                case 12:
                    FirstRandomNum = 60;
                    SecondRandomNum = 65;
                    break;

                case 13:
                    FirstRandomNum = 65;
                    SecondRandomNum = 70;
                    break;

                case 14:
                    FirstRandomNum = 70;
                    SecondRandomNum = 75;
                    break;

                default:
                    FirstRandomNum = 0;
                    SecondRandomNum = 0;
                    break;
            }
        }

        internal short CurrentQuestionNumber;
        public void ShowQuestion(Label Q,short Level)
        {
            string Question ="";

            short FirstRandomNum ;
            short SecondRandomNum;
            // ref Mandatory we must ouyside initiliaze thing then send it while : out -> allows us to send thing without initialization then mandatory inside method we must initialize it .
            _TakeRandomNumbersScaleBasedOnLevel(Level,out FirstRandomNum,out SecondRandomNum);

            if (Q.Text == "")
            {
                Q.Text = lQuestions[(CurrentQuestionNumber=clsLib.GetARandomNumber(FirstRandomNum,SecondRandomNum))].Question;               
                return;
            }
            else if (Q.Text != (Question = (Q.Text = lQuestions[(CurrentQuestionNumber=clsLib.GetARandomNumber(FirstRandomNum,SecondRandomNum))].Question)))
            {
                Q.Text = Question;
            }
            else
                ShowQuestion(Q, Level);

        }

        private short _ShuffleAnswers()
        {

            switch(clsLib.GetARandomNumber(1,7))
            {
                case 1:
                    FoundCorrectAnswer = true;
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
                case 4:
                    return 4;
                case 5:
                    return 5;
                case 6:
                    return 6;
                default:
                    return 0;
            }
        }
        
        public void ShowAnswerOnButton(Button btnAnswer,string Btn1Text,string Btn2Text,string Btn3Text)
        {
            short ShuffleNumber;
            CounterForShowingAnswers++;

            if(CounterForShowingAnswers==4)
            {
                btnAnswer.Text = lQuestions[CurrentQuestionNumber].RightAnswer;
                return;
            }

            if (((ShuffleNumber =_ShuffleAnswers()) == 1) && !FoundCorrectAnswer)
            btnAnswer.Text = lQuestions[CurrentQuestionNumber].RightAnswer;
            else
            {
                if (Btn1Text != lQuestions[CurrentQuestionNumber].WrongAnswers[ShuffleNumber - 1] && Btn2Text != lQuestions[CurrentQuestionNumber].WrongAnswers[ShuffleNumber - 1] && Btn3Text != lQuestions[CurrentQuestionNumber].WrongAnswers[ShuffleNumber - 1])
                    btnAnswer.Text = lQuestions[CurrentQuestionNumber].WrongAnswers[ShuffleNumber - 1];
                else
                {
                    ShuffleNumber = _ShuffleAnswers();
                    ShowAnswerOnButton(btnAnswer, Btn1Text, Btn2Text, Btn3Text);
                }
            }

        }


    }
}
