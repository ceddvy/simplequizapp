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
    public partial class Questions : UserControl
    {
        public Questions()
        {
            InitializeComponent();
            choiceRadioButtons = new List<RadioButton>();
        }

        private void Questions_Load(object sender, EventArgs e)
        {

        }
        private List<RadioButton> choiceRadioButtons;
        public string _question;

        public string Question
        {
            get { return _question; }
            set { _question = value;
                lblQuestion.Text = value;
            }
        }

        public List<string> Choices
        {
            set
            {
                // Clear existing radio buttons
                foreach (var radioButton in choiceRadioButtons)
                {
                    Controls.Remove(radioButton);
                }
                choiceRadioButtons.Clear();
                
                // Add new radio buttons for each choice
                foreach (var choice in value)
                {
                    var radioButton = new RadioButton
                    {
                        Text = choice,
                        Dock = DockStyle.Bottom,  
                        Font = new Font("Arial", 10)

                };
            
                    Controls.Add(radioButton);                 
                    choiceRadioButtons.Add(radioButton);
                }
                value.Reverse();
            }
        }

        public bool HasAnswer()
        {
            foreach (var radioButton in choiceRadioButtons)
            {
                if (radioButton.Checked)
                {
                    return true; // At least one radio button is checked
                }
            }

            return false; // No radio button is checked
        }

    }
}
