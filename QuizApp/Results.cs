using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        private void Results_Load(object sender, EventArgs e)
        {
            LetterCounter();
            GetResult();
            List<StringItem> items = new List<StringItem>
            {
                new StringItem { Value = "Self-Management You manage yourself well; You take responsibility for your own behavior and well-being."},
                new StringItem { Value ="Empathy You are emphatic. You see yourself in someone else’s situation before doing decisions. You tend to listen to other’s voices."},
                new StringItem { Value = "Self-Awareness You are conscious of your own character, feelings, motives, and desires. The process can be painful but it leads to greater self-awareness."}
            };

            

            dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 12); // Adjust the font size
            dataGridView2.RowTemplate.Height = 50; // Adjust the row height
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ScrollBars = ScrollBars.Both;
            dataGridView2.DataSource = items;

        }

        /// <summary>
        /// This will add the data to the data grid view
        /// 
        /// </summary>
        /// <param name="question"></param>
        /// <param name="answer"></param>
        public void AddAnswer(int question, char answer)
        {
            dataGridView1.Rows.Add(question, answer);
        }
        Dictionary<char, int> letterCount;
        /// <summary>
        /// This method will do the counter and print to the field
        /// </summary>
        private void LetterCounter()
        {
            int colIndex = 1;

           letterCount = new Dictionary<char, int>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[colIndex].Value != null)
                {
                    string valueCell = row.Cells[colIndex].Value.ToString();
                    foreach (char letter in valueCell)
                    {
                        char lowerCaseLetter = char.ToLower(letter);
                        if (letterCount.ContainsKey(lowerCaseLetter))
                        {

                            letterCount[lowerCaseLetter]++;
                        }
                        else
                        {
                            letterCount[lowerCaseLetter] = 1;
                        }
                    }
                }
            }
            StringBuilder resultBuilder = new StringBuilder();

            //This print the letter counter 
            foreach (var pair in letterCount.OrderBy(p => p.Key))
            {
                resultBuilder.AppendLine($"{pair.Key.ToString().ToUpper()}   : \t{pair.Value}");              
                
            }

            aCount.Text = resultBuilder.ToString();
        }

        /// <summary>
        /// This method will get the result
        /// based on the highest count
        /// </summary>
        private void GetResult()
        {         
            int highestValue = letterCount.Values.Max();

            bool multipleHigh = letterCount.Count(p => p.Value == highestValue)>1;

            if (multipleHigh)
            {
                resultField.Text = "Self-Awareness You are conscious of your own character, feelings, motives, and desires." +
                    "The process can be painful but it leads to greater self-awareness.";
            }
            else
            {
                var highestScore = letterCount.OrderByDescending(p => p.Value).FirstOrDefault();
                if (highestScore.Key != default(char))
                {

                    char val = highestScore.Key;

                    if (val == 'a')
                    {
                        resultField.Text = "Empathy You are emphatic. You see yourself in someone else’s situation before doing decisions. You tend to listen to other’s voices.";
                    }
                    else if (val == 'b')
                    {
                        resultField.Text = "Self-Awareness You are conscious of your own character, feelings, motives, and desires.The process can be painful but it leads to greater self-awareness.";
                    }
                    else if (val == 'c')
                    {
                        resultField.Text = "Self-Management You manage yourself well; You take responsibility for your own behavior\r\nand well-being.";
                    }

                }
            }
        }


    }
}


public class StringItem
{
    public string Value { get; set; }
}