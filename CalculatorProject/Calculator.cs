using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorProject
{
    class Calculator
    {
        //private members of the Calculator class
        private double left;    //the left operand in an equation
        private double right;   //the right operand in an equation
        private String op;  //the operator in an equation
        private String result;  //the result of an equation as a string, can also be an error message
        private Queue<String> previousResults;  //queue containing the 10 most recent results along with the current result 

        //constructor
        public Calculator(String _left, String _right, String _op)
        {
            previousResults = new Queue<String>(11);    //initialize the queue with a capcity of 11
            result = "";

            //set members left, right, op and result
            if (setLeft(_left))
            {
                if (setRight(_right))
                {
                    if (setOperator(_op))
                    {
                        result = "";
                    }
                }
            }
        }

        //public method to set the left operand
        public bool setLeft(String value)
        {
            result = "";
            //try to parse value into a Double and store the result in left
            bool success = Double.TryParse(value, out left);
            
            //if the conversion was successful, and the converted number is not Double.NaN
            if (success && !Double.IsNaN(left))
            {
                return true;    //return true;
            }
            //if value is an operand
            else if(value == "+" || value == "-" || value == "*" || value == "/")
            {
                result = "Error: Missing left operand";
            }
            else
            {
                result = String.Format("Error: \"{0}\" is not a number", value);
            }

            return false;   //else, return false
        }

        //public method to set the right operand
        public bool setRight(String value)
        {
            //try to parse value into a Double and store the result in right
            bool success = Double.TryParse(value, out right);

            //if the conversion was successful, and the converted number is not Double.NaN
            if (success && !Double.IsNaN(right))
            {
                return true;    //return true;
            }

            if(result == "")
            {
                if (value == "")
                {
                    result = "Error: Missing right operand";
                }
                else
                {
                    result = String.Format("Error: \"{0}\" is not a number", value);
                }
            }

            return false;   //else, return false
        }

        //public method to set the operator
        public bool setOperator(String value)
        {
            //if value is any one of +,-,*,/
            if (value == "+" || value == "-" || value == "*" || value == "/")
            {
                op = value; //set op to value
                return true;    //return true
            }

            if (result == "")
            {
                if(value == "")
                {
                    result = "Error: Missing operator";
                }
                else
                {
                    result = String.Format("Error: \"{0}\" is not an operator", value);
                }
            }

            return false;   //return false
        }

        //public method to calculate the result of <left><operator><right>
        public void calculate()
        {
            //if there are no already exsisting errors
            //if(result != "Left Operand Error" && result != "Right Operand Error" && result != "Operator Error")
            if(result == "")
            {
                //if the opperation is addition
                if (op == "+")
                {
                    //try-catch in case of overflow
                    try
                    {
                        //if the result is positive or negative infinity
                        if (Double.IsInfinity(left + right))
                        {
                            throw new OverflowException();  //throw an overflow exception
                        }
                        result = (left + right).ToString();   //set result as the value of left + right as a string
                    }
                    catch (OverflowException)
                    {
                        result = "Error: Overflow";  //set result as an overflow error
                    }
                }
                //if the opperation is subtraction
                else if (op == "-")
                {
                    //try-catch in case of overflow
                    try
                    {
                        //if the result is positive or negative infinity
                        if (Double.IsInfinity(left - right))
                        {
                            throw new OverflowException();  //throw an overflow exception
                        }
                        result = (left - right).ToString();   //set result as the value of left - right as a string
                    }
                    catch (OverflowException)
                    {
                        result = "Error: Overflow";  //set result as an overflow error
                    }
                }
                //if the operation is multiplication
                else if (op == "*")
                {
                    //try-catch in case of overflow
                    try
                    {
                        //if the result of the multiplication is positive or negative infinity
                        if(Double.IsInfinity(left * right))
                        {
                            throw new OverflowException();  //throw an overflow exception
                        }
                        result = (left * right).ToString();   //set result as the value of left * right as a string
                    }
                    catch (OverflowException)
                    {
                        result = "Error: Overflow";  //set result as an overflow error
                    }
                }
                //if the operation is division
                else if (op == "/")
                {
                    //try-catch in case of division by zero or overflow
                    try
                    {
                        //if the dividend is zero
                        if(right == 0)
                        {
                            throw new DivideByZeroException();  //throw a divide by zero exception
                        }
                        //if the result of the division is either positive or negative infinity
                        else if (Double.IsInfinity(left / right))
                        {
                            throw new OverflowException();  //throw an overflow exception
                        }
                        result = (left / right).ToString();   //set result as the value of left / right as a string
                    }
                    catch (DivideByZeroException)
                    {
                        result = "Error: Division by Zero";    //set result as a division by zero error
                    }
                    catch (OverflowException)
                    {
                        result = "Error: Overflow";    //set result as overflow
                    }
                }
                //if the operation is none of the above
                else
                {
                    result = null;    //set result as null
                }
            }

            //if the queue is at max capacity
            if (previousResults.Count == 11)
            {
                previousResults.Dequeue();  //dequeue the result from the front of the queue
            }
            previousResults.Enqueue(result);    //enqueue the new result at the back of the queue

        }

        //public method to return the result of a calculation
        public String getResult()
        {
            return result;  //return the result
        }

        //public method to return a previous result at a given index
        public String getPreviousResult(int index)
        {
            //if the index is outside of the bounds provided in the project documentation: 1 - 10
            if(index < 1 || index > 10)
            {
                return "Index Error";   //return an error
            }

            String[] _previousResults = previousResults.ToArray();  //copy the elements of the queue to an array
            Array.Reverse(_previousResults);    //reverse the array

            //if the index we are looking at is within the bounds of the number of elements in the array
            if(index < _previousResults.Length)
            {
                return _previousResults[index]; //return the result at the index
            }

            return "";   //return an empty string
        }

        //public method to get the maximum index that can be used for the getPreviousResults method
        public int getMaximumIndex()
        {
            return previousResults.Count;
        }
    }
}
