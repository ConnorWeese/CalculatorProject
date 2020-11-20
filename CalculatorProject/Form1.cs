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
        RefactoredCalculator refactoredCalculator;
        int index;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;

            simpleCalculator = null;
            refactoredCalculator = new RefactoredCalculator();
            index = 0;
            calculatorDisplayBox.KeyPress += calculatorDisplayBox_KeyPress;

            buttonUndo.Enabled = false;
            buttonRedo.Enabled = false;
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
            buttonUndo.Enabled = true;
            buttonRedo.Enabled = false;
            index = 0;
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

        private void buttonNegative_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "-";
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text = "";
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "E";
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            if(checkRefactoredCalculator.Checked == true)
            {
                index++;
                if (index == 10 || index == refactoredCalculator.getMaximumIndex())
                {
                    buttonUndo.Enabled = false;
                }

                calculatorDisplayBox.Text = refactoredCalculator.getPreviousResult(index);
                buttonRedo.Enabled = true;
            }
            else
            {
                index++;
                if (index == 10 || index == simpleCalculator.getMaximumIndex())
                {
                    buttonUndo.Enabled = false;
                }

                calculatorDisplayBox.Text = simpleCalculator.getPreviousResult(index);
                buttonRedo.Enabled = true;
            }
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            if (checkRefactoredCalculator.Checked == true)
            {
                index--;
                if (index == 0)
                {
                    buttonRedo.Enabled = false;
                    calculatorDisplayBox.Text = refactoredCalculator.getResult();
                }
                else
                {
                    calculatorDisplayBox.Text = refactoredCalculator.getPreviousResult(index);
                }

                buttonUndo.Enabled = true;
            }
            else
            {
                index--;
                if (index == 0)
                {
                    buttonRedo.Enabled = false;
                    calculatorDisplayBox.Text = simpleCalculator.getResult();
                }
                else
                {
                    calculatorDisplayBox.Text = simpleCalculator.getPreviousResult(index);
                }

                buttonUndo.Enabled = true;
            }
        }

        private void checkRefactoredCalculator_CheckedChanged(object sender, EventArgs e)
        {
            if(checkRefactoredCalculator.Checked == true)
            {
                calculatorDisplayBox.Text = refactoredCalculator.getResult();
                if (refactoredCalculator.getMaximumIndex() == 0)
                {
                    buttonUndo.Enabled = false;
                    buttonRedo.Enabled = false;
                    index = 0;
                }
                else
                {
                    buttonUndo.Enabled = true;
                    buttonRedo.Enabled = false;
                    index = 0;
                }
            }
            else
            {
                if(simpleCalculator == null)
                {
                    calculatorDisplayBox.Text = "";
                    buttonUndo.Enabled = false;
                    buttonRedo.Enabled = false;
                    index = 0;
                }
                else
                {
                    calculatorDisplayBox.Text = simpleCalculator.getResult();
                    buttonUndo.Enabled = true;
                    buttonRedo.Enabled = false;
                    index = 0;
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(calculatorDisplayBox.Text != "")
            {
                calculatorDisplayBox.Text = calculatorDisplayBox.Text.Remove(calculatorDisplayBox.Text.Length - 1, 1);
            }
        }

        private void calculatorDisplayBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                calculatorDisplayBox.Text = getCalculationResult();
                buttonUndo.Enabled = true;
                buttonRedo.Enabled = false;
                index = 0;
            }
        }

        private List<String> parseInput()
        {
            List<String> result = new List<string>();
            char[] characters = calculatorDisplayBox.Text.ToCharArray();
            String number = "";

            //for each char in the char array
            for(int i = 0; i < characters.Length; i++)
            {
                //if the character is an operator
                if (characters[i] == '+' || characters[i] == '-' || characters[i] == '*' || characters[i] == '/')
                {
                    result.Add(number); //add the number to the result list
                    result.Add(characters[i].ToString());   //add the operator to the result list
                    number = "";    //set number back to an empty string
                }
                //if the character is a space
                else if(characters[i] == ' ')
                {
                    //if the space is not the very first or very last character
                    if(i != 0 && i != characters.Length-1)
                    {
                        //if the space is not following an operator
                        if(characters[i-1] != '+' && characters[i - 1] != '-' && characters[i - 1] != '*' && characters[i - 1] != '/')
                        {
                            //and if the character following is not an operator
                            if (characters[i + 1] != '+' && characters[i + 1] != '-' && characters[i + 1] != '*' && characters[i + 1] != '/')
                            {
                                number += characters[i];    //add the space to number
                            }
                        }
                    }
                }
                else
                {
                    number += characters[i];    //add the character to number
                }
            }
            
            //if number is not an empty string
            if(number != "")
            {
                result.Add(number); //add the final number to the result list
            }

            result = checkScientificNotation(result);   //check the result for any scientific notation that needs formatting
            result = checkNegativeNumbers(result);  //check the result for any negative numbers that need to be created

            return result;
        }

        private String getCalculationResult()
        {
            List<String> input = parseInput();

            //if the input is not empty
            if (input.Count != 0)
            {
                //if the user has specified they are using the simple calculator
                if(checkRefactoredCalculator.Checked == false)
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
                        if (simpleCalculator == null)
                        {
                            simpleCalculator = new Calculator(input[0], "", "");
                            simpleCalculator.calculate();
                            return simpleCalculator.getResult();
                        }
                        else
                        {
                            simpleCalculator.setLeft(input[0]);
                            simpleCalculator.setOperator("");
                            simpleCalculator.setRight("");
                            simpleCalculator.calculate();
                            return simpleCalculator.getResult();
                        }
                    }
                    //if the input is longer than the basic formula
                    else
                    {
                        //check for a failed scientific notation
                        foreach(String str in input)
                        {
                            if(str[str.Length-1] == 'E')
                            {
                                for(int i=0; i<str.Length-1; i++)
                                {
                                    if(!Char.IsDigit(str[i]) && str[i] != '+' && str[i] != '-' && str[i] != '*' && str[i] != '/' && str[i] != '.')
                                    {
                                        return String.Format("Syntax Error: \"{0}\"", str);
                                    }
                                }
                            }
                        }
                        return "Error: Too many arguments";
                    }
                }
                //if the user has specified they are using the refactored calculator
                else
                {
                    return refactoredCalculator.calculate(input);
                }
            }

            return "";
        }

        private List<String> checkScientificNotation(List<String> input)
        {
            List<String> output = new List<String>();   //list of strings to hold the formatted output

            //iterate over all the strings in the input list
            for(int i=0; i<input.Count; i++)
            {
                //iterate over all the characters in each string
                for(int j=0; j<input[i].Length; j++)
                {
                    //if a certain character is not a number or an operator or a decimal, and it is not the final character in the string
                    if (!char.IsDigit(input[i][j]) && input[i][j] != '+' && input[i][j] != '-' && input[i][j] != '*' && input[i][j] != '/' && input[i][j] != '.' && j != input[i].Length-1)
                    {
                        output.Add(input[i]);   //add the string to the output list
                        break;  //break out of the for loop
                    }
                    //if a certain character is not a number or an operator or a decimal, and this is the final character in the string
                    else if (!char.IsDigit(input[i][j]) && input[i][j] != '+' && input[i][j] != '-' && input[i][j] != '*' && input[i][j] != '/' && input[i][j] != '.' && j == input[i].Length - 1)
                    {
                        //if this character is E, then this might be scientific notation
                        if(input[i][j] == 'E')
                        {
                            int intResult = 0;  //int to keep the result of the following try parse

                            //if the string next to this one is either a + or a -, and the string after that is an integer
                            //      then these three strings are in scientific notation
                            if((input[i+1] == "+" || input[i + 1] == "-") && Int32.TryParse(input[i+2], out intResult))
                            {
                                String formatted = formatScientificNotation(input[i].TrimEnd('E'), input[i + 1], intResult);    //format those three strings into one
                                output.Add(formatted);  //add the formatted string to the output list
                                i = i + 2;  //increment the outer for loop counter by 2, because we have consumed those following 2 strings for the scientific notation formatting
                                break;  //break out of the for loop
                            }
                            //those three strings are not in scientific notation
                            else
                            {
                                output.Add(input[i]);
                                break;  //break out of the for loop
                            }
                        }
                        //if the final character is anything other than E
                        else
                        {
                            output.Add(input[i]);   //add the string to the output list
                            break;  //break out of the for loop
                        }
                    }
                    //if this is the final character in the string
                    else if(j == input[i].Length - 1)
                    {
                        output.Add(input[i]);   //add the string to the output list
                        break;  //break out of the for loop
                    }
                }
            }

            return output;
        }

        private String formatScientificNotation(String number, String sign, int numberOfZeros)
        {
            String formatResult = "";

            if(sign == "+")
            {
                bool decimalFlag = false;
                foreach(char c in number)
                {
                    if(c == '.')
                    {
                        decimalFlag = true;
                    }
                    else
                    {
                        formatResult += c;
                        if (decimalFlag == true)
                        {
                            numberOfZeros--;
                        }
                    }
                }

                for(int i=0; i<numberOfZeros; i++)
                {
                    formatResult += '0';
                }
            }
            else if(sign == "-")
            {
                bool decimalFlag = false;
                foreach (char c in number)
                {
                    if (c == '.')
                    {
                        decimalFlag = true;
                    }
                    else
                    {
                        formatResult += c;
                        if (decimalFlag == false)
                        {
                            numberOfZeros--;
                        }
                    }
                }

                String zeroHolder = "0.";
                for (int i = 0; i < numberOfZeros; i++)
                {
                    zeroHolder += '0';
                }

                formatResult = zeroHolder + formatResult;
            }

            return formatResult;
        }

        private List<String> checkNegativeNumbers(List<String> input)
        {
            List<String> output = new List<String>();

            for(int i=0; i<input.Count; i++)
            {
                if (input[i] == "-")
                {
                    //if the very first thing in the input list is a -, and input has more than just one string
                    if(i == 0 && i != input.Count-1)
                    {
                        output.Add("-" + input[i + 1]); //add a - to the begining of the next string
                        i++;    //incrememnt i since we just consumed the next string
                    }
                    //if the string directly before this one is an operator, and there exists something after this string
                    else if((input[i-1] == "+" || input[i - 1] == "-" || input[i - 1] == "*" || input[i - 1] == "/") && i != input.Count - 1)
                    {
                        output.Add("-" + input[i + 1]); //add a - to the begining of the next string
                        i++;    //incrememnt i since we just consumed the next string
                    }
                    //otherwise just add the string to the output list
                    else
                    {
                        output.Add(input[i]);
                    }
                }
                //if the string is anything else
                else
                {
                    output.Add(input[i]);
                }
            }

            return output;
        }

        
    }
}
