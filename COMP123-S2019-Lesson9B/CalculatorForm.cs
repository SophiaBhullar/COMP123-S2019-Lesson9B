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
        public float OutputValue { get; set; }
        public bool DecimalExists { get; set; }

        public Label ActiveLabel { get; set; }

        public CalculatorForm()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// This is the Event Handler for the form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Load(object sender, EventArgs e)
        {
            clearNumericKeyboard();
            
            ActiveLabel = null;
            NumericKeyboardPanel.Visible = false;

            Size = new Size(320, 480);
        }
        
        
        /// <summary>
        /// This is the event handler for the CalculatorForm Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculatorForm_Click(object sender, EventArgs e)
        {
            clearNumericKeyboard();

            if(ActiveLabel != null)
            {
                ActiveLabel.BackColor = Color.White;
            }

            ActiveLabel = null;
            NumericKeyboardPanel.Visible = false;
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

            int buttonValue=0;
            bool resultCondition = int.TryParse(tag, out buttonValue);
    
            // If the user pressed a number button
            if(resultCondition)
            {
                int maxSize = (DecimalExists) ? 5 : 3;

                if(OutputString == "0")
                {
                    OutputString = tag;
                }
                else
                {
                    if(OutputString.Length < maxSize)
                    {
                        OutputString += tag;

                    }
                }

                ResultLabel.Text = OutputString;
            }

            //if the user pressed a button that is not a number
            if(!resultCondition)
            {
                switch(tag)
                {
                    case "clear":
                        clearNumericKeyboard();
                        break;
                    case "back":
                        removeLastCharacterFromResultLabel();
                        break;
                    case "done":
                        finalizeOutput();
                        break;
                    case "decimal":
                        addDecimalToResultLabel();
                        break;
                }
            }
            
        }

        /// <summary>
        /// This method adds the decimal point to Result Label
        /// </summary>
        private void addDecimalToResultLabel()
        {
            if (!DecimalExists)
            {
                OutputString += ".";
                DecimalExists = true;
            }
        }

        /// <summary>
        /// This method finalizes and converts the OutputString to a floating point value
        /// </summary>
        private void finalizeOutput()
        {
            OutputValue = float.Parse(OutputString);

            OutputValue = (float) (Math.Round(OutputValue, 1));

            if (OutputValue < 0.1f)
            {
                OutputValue = 0.1f;
            }
            ActiveLabel.Text = OutputValue.ToString();
            clearNumericKeyboard();
            NumericKeyboardPanel.Visible = false;

            ActiveLabel.BackColor = Color.White;
            ActiveLabel = null;
        }

        /// <summary>
        /// This method removes the last character from the Result Label
        /// </summary>
        private void removeLastCharacterFromResultLabel()
        {
            var lastChar = OutputString.Substring(OutputString.Length - 1);
            if (lastChar == ".")
            {
                DecimalExists = false;
            }
            OutputString = OutputString.Remove(OutputString.Length - 1);

            if (OutputString.Length == 0)
            {
                OutputString = "0";
            }
            ResultLabel.Text = OutputString;
        }

        /// <summary>
        /// This method resets the numeric keyboard and related variables
        /// </summary>
        private void clearNumericKeyboard()
        {
            ResultLabel.Text = "0";
            OutputString = "0";
            OutputValue = 0.0f;
            DecimalExists = false;
        }

        


        /// <summary>
        /// This is the Event Handler for HeightLabel click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActiveLabel_Click(object sender, EventArgs e)
        {
            if(ActiveLabel != null)
            {
                ActiveLabel.BackColor = Color.White;
                ActiveLabel = null;
            }

            ActiveLabel = sender as Label;

            ActiveLabel.BackColor = Color.LightBlue;

            NumericKeyboardPanel.Visible = true;

            if(ActiveLabel.Text != "0")
            {
                ResultLabel.Text = ActiveLabel.Text;
                OutputString = ActiveLabel.Text;
            }

            //CalculatorButtonTableLayoutPanel.Location = new Point(12, ActiveLabel.Location.Y + 55);
            NumericKeyboardPanel.BringToFront();

            AnimationTimer.Enabled = true;
        }


        /// <summary>
        /// This is the event handlet for AnimationTimer click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            var currentLocation = NumericKeyboardPanel.Location;

            //decrement current location of Numeric Keyboard by 20
            currentLocation = new Point(currentLocation.X, currentLocation.Y - 20);

            NumericKeyboardPanel.Location = currentLocation;

            //compare NumericKeyboard current location wiyth the Active Label
            if(currentLocation.Y <= ActiveLabel.Location.Y + 55)
            {
                NumericKeyboardPanel.Location = new Point(currentLocation.X, ActiveLabel.Location.Y + 55);
                AnimationTimer.Enabled = false;
            }
        }
    }
}
