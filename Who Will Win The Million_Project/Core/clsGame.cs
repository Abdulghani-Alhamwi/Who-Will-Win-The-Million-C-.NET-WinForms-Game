using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;
using System.ComponentModel;
using MyLib;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace Game
{
    internal class clsGame
    {
        public clsGame()
        {
            InitializeData();
        }
        public short CounterForShowingAnswers;
       public bool FoundCorrectAnswer =false;
        internal class clsQuestion
        {
            public string[,] Questions = new string[5, 15];
            public string[,] RigthAnswers = new string[5, 15];
            public List<string> WrongAnswers=new List<string>();
            public string CurrentQRightAnswer;

            public short ChoosedQuestion;
        }

        public clsQuestion Que =new clsQuestion();

        private void _Fill2DArrayWithData(string[,] Questions,string Question,short CurrentLevel, short CurrentRow)
        {
                Questions[CurrentRow, CurrentLevel] = Question;
        }
        private void _Fill2DArrayWithRightAnswers(string[,] RAnswersArr, string RightAnswer, short CurrentLevel, short CurrentRow)
        {
            RAnswersArr[CurrentRow, CurrentLevel] = RightAnswer;
        }
        private void _Fill2DArrayWithWrongAnswers(List<string> WrongAnswers, string [] Answers,short Counter = 0)
        {
            WrongAnswers.Add(Answers[Counter + 2]);
            Counter++;
            if (Counter == 6)
                return;
 
            _Fill2DArrayWithWrongAnswers(WrongAnswers, Answers,Counter);
        }
        private void _LoadDataFromFile(string FileName,string [,]Questions , string [,]RightAnswers,List<string> WrongAnswers, string QuestionLevelSepartor,char QueSeparator,short RowsQNumber,short ColsLevelNumber)
        {
            short CurrentLevel = 0;
            short CurrentRow=0;

            if (!File.Exists(FileName))
                return;

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
                    _Fill2DArrayWithData(Questions, QuestionWithAnswers[0], CurrentLevel, CurrentRow);
                    _Fill2DArrayWithRightAnswers(RightAnswers, QuestionWithAnswers[1], CurrentLevel, CurrentRow);
                    _Fill2DArrayWithWrongAnswers(WrongAnswers, QuestionWithAnswers);
                    CurrentRow++;

                    if (CurrentRow == 5)
                    {
                        CurrentLevel++;
                    }
                }
            }
        }
        public void InitializeData()
        {
            _LoadDataFromFile("FileQuestions.txt", Que.Questions, Que.RigthAnswers, Que.WrongAnswers, "#::#", ';', 5, 15);

        }
        public void ShowQuestion(Label Q,short Level)
        {
            string Question="";
            if (Q.Text == null)
            {
                Q.Text = Que.Questions[Que.ChoosedQuestion = clsLib.GetARandomNumber(0, 5), Level];
                return;
            }
            else if (Q.Text != (Question = Que.Questions[Que.ChoosedQuestion = clsLib.GetARandomNumber(0, 5), Level]))
            {
                Q.Text = Question;
            }
            else
                ShowQuestion(Q, Level);

        }

        private  string _ShuffleRightWrongAnswers(string RightAnswer,string RandWrongAnswer)
        {
            if (FoundCorrectAnswer)
                return RandWrongAnswer;

            switch (clsLib.GetARandomNumber(1,3))
            {
                case 1:
                    FoundCorrectAnswer = true;
                    return RightAnswer;

                    case 2:
                    return RandWrongAnswer;

               
            }
            return null;
        }
        public void ShowAnswer(Button Answer, short Level)
        {
            Que.CurrentQRightAnswer = Que.RigthAnswers[Que.ChoosedQuestion, Level];

            if (!FoundCorrectAnswer && CounterForShowingAnswers == 3)
    {
                FoundCorrectAnswer = true;
        Answer.Text = Que.RigthAnswers[Que.ChoosedQuestion, Level];
        CounterForShowingAnswers++;
    }
    else if (CounterForShowingAnswers < 4)
    {

         Answer.Text = _ShuffleRightWrongAnswers(Que.RigthAnswers[Que.ChoosedQuestion, Level], Que.WrongAnswers[clsLib.GetARandomNumber(0, Convert.ToInt16(450))]);
              
        CounterForShowingAnswers++;
    }

        }


    }
}
