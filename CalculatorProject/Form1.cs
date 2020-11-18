using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorProject
{
    public partial class Form1 : Form
    {
        Calculator simpleCalculator;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            simpleCalculator = null;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += ".";
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "0";
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text = getCalculationResult();
        }

        private void buttonOne_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "1";
        }

        private void buttonTwo_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "2";
        }

        private void buttonThree_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "3";
        }

        private void buttonFour_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "4";
        }

        private void buttonFive_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "5";
        }

        private void buttonSix_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "6";
        }

        private void buttonSeven_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "7";
        }

        private void buttonEight_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "8";
        }

        private void buttonNine_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "9";
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += " + ";
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += " - ";
        }

        private void buttonStar_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += " * ";
        }

        private void buttonSlash_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += " / ";
        }

        private List<String> parseInput()
        {
            List<String> result = new List<string>();
            char[] characters = calculatorDisplayBox.Text.ToCharArray();
            String number = "";

            //for each char in the char array
            foreach(char c in characters)
            {
                //if the char is not a space
                if(c != ' ')
                {
                    //if the character is an operator
                    if (c == '+' || c == '-' || c == '*' || c == '/')
                    {
                        result.Add(number); //add the number to the result list
                        result.Add(c.ToString());   //add the operator to the result list
                        number = "";    //set number back to an empty string
                    }
                    else
                    {
                        number += c;    //add the character to number
                    }
                }
            }
            
            //if number is not an empty string
            if(number != "")
            {
                result.Add(number); //add the final number to the result list
            }

            return result;
        }

        private String getCalculationResult()
        {
            List<String> input = parseInput();

            //if the input is not empty
            if (input.Count != 0)
            {
                //if the input contains all 3 parts: (number operator number)
                if (input.Count == 3)
                {
                    if (simpleCalculator == null)
                    {
                        simpleCalculator = new Calculator(input[0], input[2], input[1]);
                        simpleCalculator.calculate();
                        return simpleCalculator.getResult();
                    }
                    else
                    {
                        simpleCalculator.setLeft(input[0]);
                        simpleCalculator.setOperator(input[1]);
                        simpleCalculator.setRight(input[2]);
                        simpleCalculator.calculate();
                        return simpleCalculator.getResult();
                    }
                }
                //if the input only contains 2 parts: (number operator) or (operator number)
                else if (input.Count == 2)
                {
                    if (simpleCalculator == null)
                    {
                        simpleCalculator = new Calculator(input[0], "", input[1]);
                        simpleCalculator.calculate();
                        return simpleCalculator.getResult();
                    }
                    else
                    {
                        simpleCalculator.setLeft(input[0]);
                        simpleCalculator.setOperator(input[1]);
                        simpleCalculator.setRight("");
                        simpleCalculator.calculate();
                        return simpleCalculator.getResult();
                    }
                }
                //if the input only contains one part
                else if (input.Count == 1)
                {
                    //do nothing, if an operator is the only part then an error gets thrown for the left operand
                    //therefore the only part in the input textbox is a number, so just keep the number in the textbox: i.e. 137 = 137
                    return calculatorDisplayBox.Text;
                }
                //if the input is longer than the basic formula
                else
                {
                    //advanced calculator
                    return "";
                }
            }

            return "";
        }
    }
}
