using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace QuizApp
{
    public partial class MainForm : Form
    {
        List<Question> questionsList;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData();
            
        }
        
        /// <summary>
        /// Load data in the form
        /// Also can add Question and Choices
        /// </summary>
        private void LoadData()
        { 
            //Adding question and choice to the QuestionList
            questionsList = new List<Question>();

            questionsList.Add(new Question("You went to a party last night and when you arrived to school the next day, " +
                "\neverybody is talking about something you didn’t do. What will you do?",
                new List<string> { "A. Avoid everything and go with your friends",
                    "B. Go and talk with the person that started the rumors", "C. Go and talk with the teacher"}));

            questionsList.Add(new Question("What quality do you excel the most?",
                new List<string> { "A. Empathy",
                    "B. Curiosity", "C. Perserverance"}));

            questionsList.Add(new Question("You are walking down the street when you see an old lady trying to cross, " +
                "\nwhat will you do?",
                new List<string> { "A. Go and help her",
                    "B. Go for a policeman and ask him to help", "C. Keep walking ahead"}));

            questionsList.Add(new Question("You had a very difficult day at school, you will maintain a ____ attitude",
                new List<string> { "A. Depends on the situation",
                    "B. Positive", "C. Negative"}));

            questionsList.Add(new Question("You are at a party and a friend of yours comes over and offers you a drink, " +
                "\nwhat do you do?",
                new List<string> { "A. Say no thanks",
                    "B. Drink it until it is finished", "C. Ignore him and get angry at him"}));

            questionsList.Add(new Question("You just started in a new school, you will...",
                new List<string> { "A. Go and talk with the person next to you",
                    "B. Wait until someone comes over you", "C. Not talk to anyone"}));

            questionsList.Add(new Question("In a typical Friday, you would like to..",
                new List<string> { "A. Go out with your closefriends to eat",
                    "B. Go to a social club and meet more people", "C. Invite one of your friends to your house"}));

            questionsList.Add(new Question("Your relationship with your parents is..",
                new List<string> { "A. I like both equally",
                    "B. I like both equally", "C. I like my Mom the most"}));


            var random = new Random();
            questionsList = questionsList.OrderBy(_ => random.Next()).ToList();

            //Adding dynaimic user form
            Questions[] questions = new Questions[questionsList.Count];
            int questionNumber = 1;

            //Loop through question and load the question control  
            foreach (Question que in questionsList)
            {
                var question = new Questions();
                question.Question = $"{questionNumber}. {que.Text} ";
                question.Choices = que.Choices;
                questionsPanel.Controls.Add(question);
                questionNumber++;

            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool answeredQuestion = true;
            Results res = new Results();
            int ques = 0;
            char letter = '\0';
            foreach (Questions question in questionsPanel.Controls)
            {
                if (!question.HasAnswer())
                {
                    answeredQuestion = false;
                    
                }
                // Get the value of letter of the radio check

                foreach (var radioButton in question.Controls.OfType<RadioButton>())
                {
                    if (radioButton.Checked)
                    {
                        
                        letter = radioButton.Text.FirstOrDefault();

                        break;
                    }
                }
               

                if (letter != '\0')
                {
                    ques++;
                    res.AddAnswer(ques, letter);
                }

            }
            //Condition if question is already been answer
            if (answeredQuestion)
            {
                MessageBox.Show("You Answer all Questions");
                res.Show();
            }
            else
            {
                MessageBox.Show("Please Answer all Questions. Some field are empty.");
            }
        }
    }
    
}
/// <summary>
/// Class for creating multiple question
/// with choice
/// </summary>
public class Question
{
    public string Text { get; set; }
    public List<string> Choices { get; set; }

    public Question(string text, List<string> choices)
    {
        Text = text;
        Choices = choices;
    }
}


