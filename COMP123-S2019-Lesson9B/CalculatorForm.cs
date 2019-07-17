using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP123_S2019_Lesson9B
{
    public partial class CalculatorForm : Form
    {
        //CLASS PROPERTIES
        public string  OutputString { get; set; }
        public bool DecimalExists { get; set; }

        public CalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is the event handler for all the calculator buttons- Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorButton_Click(object sender, EventArgs e)
        {
            var TheButton = sender as Button;

            var tag = TheButton.Tag.ToString();

            int buttonValue;
            bool resultCondition = int.TryParse(tag, out buttonValue);
    
            // If the user pressed a number button
            if(resultCondition)
            {
                OutputString += tag;
                ResultLabel.Text = OutputString;
            }

            //if the user pressed a button that is not a number
            if(!resultCondition)
            {
                switch(tag)
                {
                    case "clear":
                        ResultLabel.Text = "0";
                        OutputString = string.Empty;
                        DecimalExists = false;
                        break;
                    case "back":
                        var lastChar = OutputString.Substring(OutputString.Length - 1);
                        if(lastChar == ".")
                        {
                            DecimalExists = false;
                            OutputString=  OutputString.Remove(OutputString.Length - 1);
                        }
                        break;
                    case "done":
                        break;
                    case "decimal":
                        if(!DecimalExists)
                        {
                            if(ResultLabel.Text == "0")
                            {
                                OutputString += "0";
                            }
                            OutputString += ".";
                            DecimalExists = true;
                        }
                        break;
                }
            }
            
        }

        private void CalculatorButtonTableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CalculatorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
