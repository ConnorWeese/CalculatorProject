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
        Calculator simpleCalculator;    //simple calculator
        RefactoredCalculator refactoredCalculator;  //refactored calculator
        int index;  //index used to get previous results from both calculators

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;  //set the form's boarder style to FixedDialog

            simpleCalculator = null;    //set the simpleCalculator to null
            refactoredCalculator = new RefactoredCalculator();  //initialize the refactoredCalculator
            index = 0;  //set the history index to 0
            calculatorDisplayBox.KeyPress += calculatorDisplayBox_KeyPress; //add the new keypress event to the textbox

            buttonUndo.Enabled = false; //set the undo button to be disabled
            buttonRedo.Enabled = false; //set the redo button to be disabled
        }

        //GUI event handlers

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
            String calcResult = getCalculationResult(); //string to hold the results of the calculation

            //if the user has selected to use the refactored calculator
            if (checkRefactoredCalculator.Checked == true) 
            {
                //if the ressult returned by the calculation is not the same as the result stored by the calculator
                if (calcResult == refactoredCalculator.getResult())
                {
                    calculatorDisplayBox.Text = calcResult;
                    buttonUndo.Enabled = true;  //set the undo button to be enabled
                    buttonRedo.Enabled = false; //set the redo button to be disabled
                    index = 0;  //set the history index to 0
                }
                else
                {
                    calculatorDisplayBox.Text = calcResult;
                }
            }
            //if the user has selected to use the simple calculator
            else
            {
                //if the ressult returned by the calculation is not the same as the result stored by the calculator
                if (simpleCalculator != null && calcResult == simpleCalculator.getResult())
                {
                    calculatorDisplayBox.Text = calcResult;
                    buttonUndo.Enabled = true;  //set the undo button to be enabled
                    buttonRedo.Enabled = false; //set the redo button to be disabled
                    index = 0;  //set the history index to 0
                }
                else
                {
                    calculatorDisplayBox.Text = calcResult;
                }
            }

            //focus on the calculatorDisplayBox and set the cursor to the end
            calculatorDisplayBox.Focus();
            calculatorDisplayBox.SelectionStart = calculatorDisplayBox.TextLength;
            calculatorDisplayBox.SelectionLength = 0;
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

            //focus on the calculatorDisplayBox and set the cursor to the end
            calculatorDisplayBox.Focus();
            calculatorDisplayBox.SelectionStart = calculatorDisplayBox.TextLength;
            calculatorDisplayBox.SelectionLength = 0;
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            calculatorDisplayBox.Text += "E";
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            //if this is the refactored calculator
            if(checkRefactoredCalculator.Checked == true)
            {
                index++;    //increment the index by 1
                if (index == 10 || index == refactoredCalculator.getMaximumIndex()) //if the index is at the end of the previous results
                {
                    buttonUndo.Enabled = false; //disbale the undo button
                }

                calculatorDisplayBox.Text = refactoredCalculator.getPreviousResult(index);  //set the calculatorDisplayBox to be the previous result
                buttonRedo.Enabled = true;  //enable the redo button
            }
            //if this is the simple calculator
            else
            {
                index++;    //increment the index by 1
                if (index == 10 || index == simpleCalculator.getMaximumIndex()) //if the index is at the end of the previous results
                {
                    buttonUndo.Enabled = false; //disbale the undo button
                }

                calculatorDisplayBox.Text = simpleCalculator.getPreviousResult(index);  //set the calculatorDisplayBox to be the previous result
                buttonRedo.Enabled = true;  //enable the redo button
            }
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
            //if this is the refactored calculator
            if (checkRefactoredCalculator.Checked == true)
            {
                index--;    //decrement the index
                if (index == 0) //if the index is now at 0
                {
                    buttonRedo.Enabled = false; //diable the redo button
                    calculatorDisplayBox.Text = refactoredCalculator.getResult();   //get the current result
                }
                else
                {
                    calculatorDisplayBox.Text = refactoredCalculator.getPreviousResult(index);  //get the previous result
                }

                buttonUndo.Enabled = true;  //enable the undo button
            }
            //if this is the simple calculator
            else
            {
                index--;    //decrement the index
                if (index == 0) //if the index is now at 0
                {
                    buttonRedo.Enabled = false; //diable the redo button
                    calculatorDisplayBox.Text = simpleCalculator.getResult();   //get the current result
                }
                else
                {
                    calculatorDisplayBox.Text = simpleCalculator.getPreviousResult(index);  //get the previous result
                }

                buttonUndo.Enabled = true;  //enable the undo button
            }
        }

        private void checkRefactoredCalculator_CheckedChanged(object sender, EventArgs e)
        {
            //if the user has selected to use the refactored calculator
            if(checkRefactoredCalculator.Checked == true)
            {
                calculatorDisplayBox.Text = refactoredCalculator.getResult();   //set the calculatorDisplayBox to be the current result
                //if the calculator has no history
                if (refactoredCalculator.getMaximumIndex() == 0)
                {
                    buttonUndo.Enabled = false; //disable the undo button
                    buttonRedo.Enabled = false; //disable the redo button
                    index = 0;  //set the history index to 0
                }
                else
                {
                    buttonUndo.Enabled = true;  //enable the undo button
                    buttonRedo.Enabled = false; //diable the redo button
                    index = 0;  //set the history index to 0
                }
            }
            //if the user has de-selected the refactored calculator
            else
            {
                //if the simple calculator has not yet been used
                if(simpleCalculator == null)
                {
                    calculatorDisplayBox.Text = ""; //set the calculatorDisplayBox to an empty string
                    buttonUndo.Enabled = false; //disable the undo button
                    buttonRedo.Enabled = false; //disable the redo button
                    index = 0; //set the history index to 0
                }
                else
                {
                    calculatorDisplayBox.Text = simpleCalculator.getResult();   //set the calculatorDisplayBox to be the current result
                    buttonUndo.Enabled = true;  //enable the undo button
                    buttonRedo.Enabled = false; //diable the redo button
                    index = 0;  //set the history index to 0
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            //if there is text to remove
            if(calculatorDisplayBox.Text != "")
            {
                calculatorDisplayBox.Text = calculatorDisplayBox.Text.Remove(calculatorDisplayBox.TextLength - 1, 1);   //remove one character from the string
            }

            //focus on the calculatorDisplayBox and set the cursor to the end
            calculatorDisplayBox.Focus();
            calculatorDisplayBox.SelectionStart = calculatorDisplayBox.TextLength;
            calculatorDisplayBox.SelectionLength = 0;
        }

        private void calculatorDisplayBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if the user presses enter when the textbox is focused
            if(e.KeyChar == (char)Keys.Enter)
            {
                String calcResult = getCalculationResult(); //string to hold the results of the calculation

                //if the user has selected to use the refactored calculator
                if (checkRefactoredCalculator.Checked == true)
                {
                    //if the ressult returned by the calculation is not the same as the result stored by the calculator
                    if (calcResult == refactoredCalculator.getResult())
                    {
                        calculatorDisplayBox.Text = calcResult;
                        buttonUndo.Enabled = true;  //set the undo button to be enabled
                        buttonRedo.Enabled = false; //set the redo button to be disabled
                        index = 0;  //set the history index to 0
                    }
                    else
                    {
                        calculatorDisplayBox.Text = calcResult;
                    }
                }
                //if the user has selected to use the simple calculator
                else
                {
                    //if the ressult returned by the calculation is not the same as the result stored by the calculator
                    if (simpleCalculator != null && calcResult == simpleCalculator.getResult())
                    {
                        calculatorDisplayBox.Text = calcResult;
                        buttonUndo.Enabled = true;  //set the undo button to be enabled
                        buttonRedo.Enabled = false; //set the redo button to be disabled
                        index = 0;  //set the history index to 0
                    }
                    else
                    {
                        calculatorDisplayBox.Text = calcResult;
                    }
                }

                //focus on the calculatorDisplayBox and set the cursor to the end
                calculatorDisplayBox.Focus();
                calculatorDisplayBox.SelectionStart = calculatorDisplayBox.TextLength;
                calculatorDisplayBox.SelectionLength = 0;
            }
        }

        //private method to parse the input for the calculator
        private List<String> parseInput()
        {
            List<String> result = new List<string>();   //list of strings to hold the result of the parse
            char[] characters = calculatorDisplayBox.Text.ToCharArray();    //character array that holds the contents of the calculatorDisplayBox
            String number = ""; //a string to hold a number that is being parsed

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

        //private method to get the calculation results from the calculator
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
                        //if the simpleCalculator has not been initiated yet
                        if (simpleCalculator == null)
                        {
                            simpleCalculator = new Calculator(input[0], input[2], input[1]);    //call the constructor
                            simpleCalculator.calculate();   //calculate the result
                            return simpleCalculator.getResult();    //return the result
                        }
                        else
                        {
                            simpleCalculator.setLeft(input[0]); //set the left operand
                            simpleCalculator.setOperator(input[1]); //set the operator
                            simpleCalculator.setRight(input[2]);    //set the right operand
                            simpleCalculator.calculate();   //calculate the result
                            return simpleCalculator.getResult();    //return the result
                        }
                    }
                    //if the input only contains 2 parts: (number operator) or (operator number)
                    else if (input.Count == 2)
                    {
                        //if the simpleCalculator has not been initiated yet
                        if (simpleCalculator == null)
                        {
                            simpleCalculator = new Calculator(input[0], "", input[1]);  //call the constructor
                            simpleCalculator.calculate();   //calculate the result
                            return simpleCalculator.getResult();    //return the result
                        }
                        else
                        {
                            simpleCalculator.setLeft(input[0]); //set the left operand
                            simpleCalculator.setOperator(input[1]); //set the operator
                            simpleCalculator.setRight("");  //set the right operand to be an empty string
                            simpleCalculator.calculate();   //calculate the result
                            return simpleCalculator.getResult();    //return the result
                        }
                    }
                    //if the input only contains one part
                    else if (input.Count == 1)
                    {
                        /*
                        //if the simpleCalculator has not been initiated yet
                        if (simpleCalculator == null)
                        {
                            simpleCalculator = new Calculator(input[0], "", "");    //call the constructor
                            simpleCalculator.calculate();   //calculate the result
                            return simpleCalculator.getResult();    //return the result
                        }
                        else
                        {
                            simpleCalculator.setLeft(input[0]); //set the left operand
                            simpleCalculator.setOperator("");   //set the operator to be an empty string
                            simpleCalculator.setRight("");  //set the right operand to be an empty string
                            simpleCalculator.calculate();   //calculate the reuslt
                            return simpleCalculator.getResult();    //return the result
                        }*/
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
                                        return String.Format("Error: \"{0}\" is not a number", str);
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
                    return refactoredCalculator.calculate(input);   //return the result of the calculation
                }
            }

            return calculatorDisplayBox.Text;
        }

        //private method to check if some strings are in scientific notation format
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
                    //if a certain character is not a number or an operator or a decimal, this is the final character in the string, this is not the only character in the string, and this is not the last string in input
                    else if (!char.IsDigit(input[i][j]) && input[i][j] != '+' && input[i][j] != '-' && input[i][j] != '*' && input[i][j] != '/' && input[i][j] != '.' && j == input[i].Length - 1 && j != 0 && i != input.Count-1)
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

        //private method to format scientific notation to standard notation for use in the calculator
        private String formatScientificNotation(String number, String sign, int numberOfZeros)
        {
            String formatResult = "";   //string to hold the result of the format

            //if it is of the form <number>E+<number>
            if(sign == "+")
            {
                bool decimalFlag = false;   //bool used to mark when a decimal has been found
                foreach(char c in number)
                {
                    //if the character is a decimal
                    if(c == '.')
                    {
                        decimalFlag = true; //set the flag to true
                    }
                    else
                    {
                        formatResult += c;  //add the character to the format result
                        if (decimalFlag == true)    //if we have seen a decimal
                        {
                            numberOfZeros--;    //decrement the number of zeros
                        }
                    }
                }

                //iterate over the total number of zeros to add to the number
                for(int i=0; i<numberOfZeros; i++)
                {
                    formatResult += '0';    //add a zero to the end of the result
                }
            }
            //if it is of the form <number>E-<number>
            else if (sign == "-")
            {
                bool decimalFlag = false;   //bool used to mark when a decimal has been found
                foreach (char c in number)
                {
                    //if the character is a decimal
                    if (c == '.')
                    {
                        decimalFlag = true; //set the flag to true
                    }
                    else
                    {
                        formatResult += c;  //add the character to the format result
                        if (decimalFlag == false)   //if we have not yet seen a decimal
                        {
                            numberOfZeros--;    //decrement the number of zeros
                        }
                    }
                }

                String zeroHolder = "0.";   //string to hold the zeros we are adding to the result
                //iterate over the total number of zeros to add to the number
                for (int i = 0; i < numberOfZeros; i++)
                {
                    zeroHolder += '0';  //add a zero to the zero holder
                }

                formatResult = zeroHolder + formatResult;   //put the format result behind the zeros we added to the zeroHolder
            }

            return formatResult;    //return the result
        }

        //private method to check for negative numbers
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
